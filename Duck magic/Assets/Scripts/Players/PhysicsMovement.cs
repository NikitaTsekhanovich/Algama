using System;
using UnityEngine;

namespace Players
{
    public class PhysicsMovement : MonoBehaviour
    {
        // [SerializeField] private Rigidbody2D _rigidbody;
        private Rigidbody2D _rigidbody;

        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;
        
        [SerializeField] private Transform _groundCheckPoint;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private LayerMask _whatIsGround;
        private bool _isGround;

        private void Start()
        {
            _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        }

        public void MoveLeft()
        {
            _rigidbody.velocity = new Vector2(-_speed, _rigidbody.velocity.y);
            // _rigidbody.velocity = new Vector2(-_speed, _rigidbody.velocity.y);
        }
        
        public void MoveRight()
        {
            _rigidbody.velocity = new Vector2(_speed, _rigidbody.velocity.y);
        }

        public void Jump()
        {
            _isGround = Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckRadius, _whatIsGround);

            if (_isGround)
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
        }
    }
}
