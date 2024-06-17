using System.Collections.Generic;
using GameItems.Properties;
using UnityEngine;
using Photon.Pun;
using Interfaces;

namespace GameItems.DamageDealers.Dynamites
{
    public class Dynamite : MonoBehaviour, IObserver, ICanDealDamage
    {
        [SerializeField] private DamagerData _damagerData;
        [SerializeField] private GameObject _animExplosion;
        [SerializeField] private DynamiteSounds _dynamiteSounds;
        private Dictionary<int, GameObject> _playersInRange = new();
        private PhotonView _photonView;
        private SpriteRenderer _spriteRender;
        private BoxCollider2D _boxCollider;

        private void Start()
        {
            _photonView = GetComponent<PhotonView>();
            _spriteRender = GetComponent<SpriteRenderer>();
            _boxCollider = GetComponent<BoxCollider2D>();

            var healthHandler = gameObject.AddComponent<HealthHandlerItems>();
            healthHandler.Init(_damagerData.Health);
        }

        public void OnEnable()
        {
            HealthHandlerItems.OnDestroy += Explosion;
        }

        public void OnDisable()
        {
            HealthHandlerItems.OnDestroy -= Explosion;
        }

        public void Explosion(PhotonView view)
        {
            if (view.InstantiationId == _photonView.InstantiationId)
            {
                foreach (var player in _playersInRange)
                    DealDamageTo(player.Value.GetComponent<Players.HealthHandler>(), player.Value.GetComponent<PhotonView>());

                DoDestruction();
            }
        }

        private void DoDestruction()
        {
            _dynamiteSounds.ExplosionSound.Play();
            _boxCollider.enabled = false;
            _spriteRender.enabled = false;
            _animExplosion.SetActive(true);
            Destroy(gameObject, 1f);
        }

        public void DealDamageTo<TPlayerHealth>(TPlayerHealth healthHandler, PhotonView view) 
            where TPlayerHealth : Players.HealthHandler
        {
            healthHandler.OnDamage(_damagerData.Damage, view);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player") && other.GetComponent<PhotonView>().IsMine)
                _playersInRange[other.GetComponent<PhotonView>().ViewID] = other.gameObject;

        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player") && other.GetComponent<PhotonView>().IsMine)
                _playersInRange.Remove(other.GetComponent<PhotonView>().ViewID);
        }
    }
}