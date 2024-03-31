using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Players
{
    public class HealthHandler : MonoBehaviour, IObserver
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
            MagicBall.OnDamagePlayer += ChangeHealth;
        }

        public void OnDisable()
        {
            MagicBall.OnDamagePlayer -= ChangeHealth;
        }

        private void ChangeHealth(float damage, int id)
        {
            if (_settingPlayerNetwork.View.InstantiationId == id)
            {
                _health -= damage / 100f;
                _healthBar.fillAmount = _health;
            }
        }
    }
}
