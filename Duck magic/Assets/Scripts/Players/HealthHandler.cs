using Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace Players
{
    public class HealthHandler : MonoBehaviourPunCallbacks, IObserver, IPunObservable
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private PhotonView _view;
        
        private float _health;

        private void Start()
        {
            _health = _healthBar.fillAmount;
            _view = GetComponent<PhotonView>();
        }

        public void OnEnable()
        {
            MagicBall.OnDamagePlayer += ChangeHealth;
        }

        public void OnDisable()
        {
            MagicBall.OnDamagePlayer -= ChangeHealth;
        }

        private void ChangeHealth(int damage, int id)
        {
            if (_view.InstantiationId == id)
            {
                _health -= damage / 100f;
                _healthBar.fillAmount = _health;
            } 
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_health);
            }
            else
            {
                _health = (float)stream.ReceiveNext();
            }
        }
    }
}
