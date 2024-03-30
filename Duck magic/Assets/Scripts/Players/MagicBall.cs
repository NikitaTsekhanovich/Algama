using System;
using Photon.Pun;
using UnityEngine;

namespace Players
{
    public class MagicBall : MonoBehaviour
    {
        [SerializeField] private float _magicBallSpeed;
        [SerializeField] private int _damage;
        
        private Rigidbody2D _rigidbody;
        
        public static Action<int, int> OnDamagePlayer;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _rigidbody.velocity = new Vector2(_magicBallSpeed * transform.localScale.x, 0);
        }

        private void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.CompareTag("Player"))
            {
                Debug.Log($"Name: {coll.GetComponent<PhotonView>().name}");
                OnDamagePlayer?.Invoke(_damage, coll.GetComponent<PhotonView>().InstantiationId);
            }
            
            Destroy(gameObject);
        }
    }
}
