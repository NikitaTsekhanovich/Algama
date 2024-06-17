using System.Collections.Generic;
using Photon.Pun;
using Players;
using Spells.Properties;
using UnityEngine;
using GameItems.DamageDealers.Dynamites;
using System.Collections;

namespace Spells.Types
{
    public class Tornado : Spell, ICanMove, ICanDealDamage
    {
        [field: SerializeField] protected override float Lifetime { get; set; }
        [field: SerializeField] public float MoveSpeed { get; set; }
        
        public Vector2 Direction => new(transform.localScale.x, 0);
        public Rigidbody2D Rigidbody2D { get; set; }

        [field: SerializeField] public float DamageOnPlayer { get; set; }
        [field: SerializeField] public float? DamageOnProp { get; set; }

        private readonly HashSet<PhotonView> _alreadyHit = new();

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
            if (other.gameObject.CompareTag("Player"))
            {
                DealDamageTo(other.GetComponent<HealthHandler>(), 
                    other.GetComponent<PhotonView>());
            }
            
            if (LayerMask.LayerToName(other.gameObject.layer) == "Ground" && !other.CompareTag("DestroyableObject"))
            {
                Destroy(gameObject);
            }
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
                _alreadyHit.Remove(other.GetComponent<PhotonView>());
        }

        public void DealDamageTo<TPlayerHealth>(TPlayerHealth healthHandler, PhotonView view)
            where TPlayerHealth : HealthHandler
        {
            _alreadyHit.Add(view);
            StartCoroutine(DealPeriodicDamage(healthHandler));
        }

        private IEnumerator DealPeriodicDamage<TPlayerHealth>(TPlayerHealth healthHandler)
            where TPlayerHealth : HealthHandler
        {
            while (true)
            {
                foreach (var view in _alreadyHit) 
                    healthHandler.OnDamage(DamageOnPlayer, view);
                yield return new WaitForSeconds(0.1f);
            }
        }

        public void DealDestroyTo<TDestroyableObject>(TDestroyableObject healthHandler, PhotonView view) 
            where TDestroyableObject : HealthHandlerItems
        {
            healthHandler.OnDamage(DamageOnPlayer, view);
        }
    }
}