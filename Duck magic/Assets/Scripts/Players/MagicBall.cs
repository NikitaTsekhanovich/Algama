using System;
using UnityEngine;

namespace Players
{
    public class MagicBall : MonoBehaviour
    {
        [SerializeField] private float _magicBallSpeed;
        [SerializeField] private int _damage;

        private Rigidbody2D _rigidbody;
        
        public static Action<int> OnDamagePlayer;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _rigidbody.velocity = new Vector2(_magicBallSpeed * transform.localScale.x, 0);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                OnDamagePlayer?.Invoke(_damage);
            }
            Destroy(gameObject);
        }
    }
}
