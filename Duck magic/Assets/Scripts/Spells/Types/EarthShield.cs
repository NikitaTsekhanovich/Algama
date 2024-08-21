using GameItems.DamageDealers.Dynamites;
using Photon.Pun;
using Players;
using Spells.Properties;
using UnityEngine;

namespace Spells.Types
{
    public class EarthShield : Spell, ICanMove, ICanDealDamage
    {
        [field: SerializeField] public float DamageOnPlayer { get; set; }
        [field: SerializeField] public float? DamageOnProp { get; set; }
        public Rigidbody2D Rigidbody2D { get; set; }
        [field: SerializeField] public float MoveSpeed { get; set; }
        public Vector2 Direction => throw new System.NotImplementedException();

        [field: SerializeField] protected override float Lifetime { get; set; }

        public void DealDamageTo<TPlayerHealth>(TPlayerHealth healthHandler, PhotonView view) where TPlayerHealth : HealthHandler
        {
            throw new System.NotImplementedException();
        }

        public void DealDestroyTo<TDestroyableObject>(TDestroyableObject healthHandler, PhotonView view) where TDestroyableObject : HealthHandlerItems
        {
            throw new System.NotImplementedException();
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }
    }
}

