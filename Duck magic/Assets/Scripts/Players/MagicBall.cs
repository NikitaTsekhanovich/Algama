using System;
using UnityEngine;

namespace Players
{
    public class MagicBall : MonoBehaviour
    {
        [SerializeField] private float _magicBallSpeed;

        private Rigidbody2D _rigidbody;

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
            Destroy(gameObject);
        }
    }
}
