using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameItems.DamageDealers
{
    [CreateAssetMenu(fileName = "DamagerPeriodicData", menuName = "Damage items/ Damager periodicity data")]
    public class DamagerPeriodicData : Item
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _periodicity;

        public float Damage => _damage;
        public float Periodicity => _periodicity;
    }
}

