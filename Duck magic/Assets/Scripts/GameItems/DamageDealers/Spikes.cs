using System.Collections;
using System.Collections.Generic;
using GameItems.Properties;
using UnityEngine;

namespace GameItems.DamageDealers
{
    public class Spikes : MonoBehaviour, ICanDealDamageOverTime
    {
        [SerializeField] private DamagerPeriodicData _damagerData;

        public float DamagePerSeocnd {
             get => _damagerData.Periodicity;
        }

        public IEnumerator OnStay()
        {
            throw new System.NotImplementedException();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            
        }
    }
}