using Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;

namespace Players
{
    public class HealthHandler : MonoBehaviourPunCallbacks, IObserver, IPunObservable
    {
        [SerializeField] private Image _healthBar;

        public void OnEnable()
        {
            MagicBall.OnDamagePlayer += ChangeHealth;
        }

        public void OnDisable()
        {
            MagicBall.OnDamagePlayer -= ChangeHealth;
        }

        private void ChangeHealth(int damage)
        {
            _healthBar.fillAmount -= damage / 100f;
        }

        // [PunRPC]
        // void GiveDamageRPC(int damage)
        // {
        //     _healthBar.fillAmount -= damage / 100f;
        // }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(_healthBar.fillAmount);
            }
            else
            {
                // Network player, receive data
                _healthBar.fillAmount = (float)stream.ReceiveNext();
            }
        }
    }
}
