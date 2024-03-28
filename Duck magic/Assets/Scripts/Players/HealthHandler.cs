using Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Photon;

namespace Players
{
    public class HealthHandler : MonoBehaviour, IObserver
    {
        [SerializeField] private Image _healthBar;
        
        // private PhotonView _view;
        //
        // private void Start()
        // {
        //     _view = GetComponent<PhotonView>();
        // }

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
    }
}
