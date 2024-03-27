using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Players
{
    public class HealthHandler : MonoBehaviour, IObserver
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
    }
}
