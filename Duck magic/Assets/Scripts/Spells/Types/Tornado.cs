using System.Collections.Generic;
using Photon.Pun;
using Players;
using Spells.Properties;
using UnityEngine;

namespace Spells.Types
{
    public class Tornado : Spell, ICanMove, ICanDealDamage
    {
        [field: SerializeField] protected override float Lifetime { get; set; }
        [field: SerializeField] public float MoveSpeed { get; set; }
        
        public Vector2 Direction => new(transform.localScale.x, 0);
        public Rigidbody2D Rigidbody2D { get; set; }
        
        [field: SerializeField] public float DamageOnPlayer { get; set; }
        [field: SerializeField ]public float? DamageOnProp { get; set; }
        
        private void Start()
        {
            base.Start();
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            base.Update();
            Rigidbody2D.velocity = MoveSpeed * Direction;
        }
        
        public void OnTriggerEnter2D(Collider2D other)
        {
            Destroy(gameObject);

            if (other.gameObject.CompareTag("Player"))
                DealDamageTo(other.GetComponent<HealthHandler>(),
                    other.GetComponent<PhotonView>().InstantiationId);
        }

        public void DealDamageTo<TPlayerHealth>(TPlayerHealth healthHandler, int playerId)
            where TPlayerHealth : HealthHandler
        {
            healthHandler.ChangeHealth(DamageOnPlayer, playerId);
        }
    }
}