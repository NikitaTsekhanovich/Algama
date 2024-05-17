using System;
using GameObjects.MagicStones;
using System.Reflection;
using Interfaces;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Players
{
    public class HealthHandler : MonoBehaviourPunCallbacks, IObserver, IPunObservable
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private SettingPlayerNetwork _settingPlayerNetwork;
        [SerializeField] private GameObject _healthBarImage;
        private float _health;
        private bool _isAlive;

        public bool IsAlive => _isAlive;
        
        public static Action<int, string> OnDiedPlayer;

        private void Start()
        {
            _health = _healthBar.fillAmount;
            _isAlive = true;
        }

        public void OnEnable()
        {
            // MagicBall.OnDamagePlayer += OnDamage;
            HealerStone.OnHealPlayer += OnHeal;
        }

        public void OnDisable()
        {
            // MagicBall.OnDamagePlayer -= OnDamage;
            HealerStone.OnHealPlayer -= OnHeal;
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
            if (_health <= 0)
            {
                _isAlive = false;
                OnDiedPlayer?.Invoke(currentId, _settingPlayerNetwork.View.Owner.NickName);
                _healthBarImage.SetActive(false);
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
    }
}
