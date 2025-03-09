using System;
using System.Collections;
using GameItems.SupportDealers;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Players
{
    public class HealthHandler : MonoBehaviourPunCallbacks, IPunObservable
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private Image _manaBar;
        [SerializeField] private SettingPlayerNetwork _settingPlayerNetwork;
        [SerializeField] private GameObject _healthBarImage;
        [SerializeField] private GameObject _manaBarImage;
        private float _health;
        private float _manaRegen = 1f;
        private const float _maxMana = 1f;
        private bool _isAlive;
        public float Mana { get; set; }

        public bool IsAlive => _isAlive;

        public static Action<int, string> OnDiedPlayer;

        private void Start()
        {
            StartCoroutine(RegenMana());
            _health = _healthBar.fillAmount;
            Mana = _healthBar.fillAmount;
            _isAlive = true;
        }

        public void OnEnable()
        {
            HealerStone.OnHealPlayer += OnHeal;
        }

        public void OnDisable()
        {
            HealerStone.OnHealPlayer -= OnHeal;
        }

        public void OnCast(float manaCost, PhotonView view)
        {
            view.RPC("GetManaUsage", RpcTarget.AllBuffered, manaCost);
        }

        [PunRPC]
        private void GetManaUsage(float manaUsage)
        {
            Mana -= manaUsage;
            _manaBar.fillAmount = Mana;
            if (Mana <= 0)
                Mana = 0;

            if (Mana <= 10)
                _manaRegen = 0.5f;

            if (Mana > 75)
                _manaRegen = 1f;
        }

        public void OnDamage(float damage, PhotonView view)
        {
            if (view.IsMine &&
                _settingPlayerNetwork.View.InstantiationId == view.InstantiationId)
            {
                view.RPC("GetDamage", RpcTarget.AllBuffered, damage, view.InstantiationId);
            }
        }

        private void OnHeal(float heal, PhotonView view)
        {
            view.RPC("GetHeal", RpcTarget.AllBuffered, heal);
        }

        [PunRPC]
        private void GetDamage(float damage, int currentId)
        {
            _health -= damage / 100f;
            _healthBar.fillAmount = _health;
            if (_health <= 0 && _isAlive)
            {
                _isAlive = false;
                OnDiedPlayer?.Invoke(currentId, _settingPlayerNetwork.View.Owner.NickName);
                _healthBarImage.SetActive(false);
                _manaBarImage.SetActive(false);
            }
        }

        [PunRPC]
        private void GetHeal(float heal)
        {
            if (_health < 1)
            {
                _health += heal / 100f;
                _healthBar.fillAmount = _health;
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_healthBar.fillAmount);
            }
            else
            {
                _healthBar.fillAmount = (float)stream.ReceiveNext();
            }
        }

        private IEnumerator RegenMana()
        {
            while (true)
            {
                Mana = Math.Min(Mana + _manaRegen / 100f, _maxMana);
                _manaBar.fillAmount = Mana;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}