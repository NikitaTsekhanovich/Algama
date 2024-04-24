using GameObjects.MagicStones;
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
        
        private float _health;

        private void Start()
        {
            _health = _healthBar.fillAmount;
        }

        public void OnEnable()
        {
            MagicBall.OnDamagePlayer += OnDamage;
            HealerStone.OnHealPlayer += OnHeal;
        }

        public void OnDisable()
        {
            MagicBall.OnDamagePlayer -= OnDamage;
            HealerStone.OnHealPlayer -= OnHeal;
        }
        
        private void OnDamage(float damage, PhotonView view)
        {
            if (view.IsMine && _settingPlayerNetwork.View.InstantiationId == view.InstantiationId)
            {
                view.RPC("GetDamage", RpcTarget.AllBuffered, damage);
            }
        }

        private void OnHeal(float heal, PhotonView view)
        {
            view.RPC("GetHeal", RpcTarget.AllBuffered, heal);
        }

        [PunRPC]
        private void GetDamage(float damage)
        {
            _health -= damage / 100f;
            _healthBar.fillAmount = _health;
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
