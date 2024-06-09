using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameItems.DamageDealers
{
    [CreateAssetMenu(fileName = "DamagerData", menuName = "Damage items/ Damager data")]
    public class DamagerData : Item
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _health;

        public float Damage => _damage;
        public float Health => _health;
    }
}