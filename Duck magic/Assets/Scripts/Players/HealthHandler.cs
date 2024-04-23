using GameObjects.MagicStones;
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
            MagicBall.OnDamagePlayer += GetDamage;
            HealerStone.OnTreatmentPlayer += GetTreatment;
        }

        public void OnDisable()
        {
            MagicBall.OnDamagePlayer -= GetDamage;
            HealerStone.OnTreatmentPlayer -= GetTreatment;
        }

        private void GetDamage(float damage, int id)
        {
            if (_settingPlayerNetwork.View.InstantiationId == id)
            {
                _health -= damage / 100f;
                _healthBar.fillAmount = _health;
            }
        }

        private void GetTreatment(float heal, int id)
        {
            if (_settingPlayerNetwork.View.InstantiationId == id && _health < 1)
            {
                _health += heal / 100f;
                _healthBar.fillAmount = _health;
            }
        }
    }
}
