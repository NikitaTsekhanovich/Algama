using System;
using UnityEngine;

namespace Players
{
    public class PhysicsMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;
        
        [SerializeField] private Transform _groundCheckPoint;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private LayerMask _whatIsGround;
        
        private Rigidbody2D _rigidbody;
        private bool _isGround;
        private float _horizontalInput;
        private Vector2 _moveVelocity;
        
        private void Start()
        {
            _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        }
        
        public void Move(float horizontalInput)
        {
            _moveVelocity = Vector2.right * horizontalInput * _speed;
            // _rigidbody.MovePosition(_rigidbody.position + _moveVelocity * Time.fixedDeltaTime);
            _rigidbody.velocity = new Vector2(horizontalInput * _speed, _rigidbody.velocity.y);
            CheckDirectionMove();
        }

        public void Jump()
        {
            _isGround = Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckRadius, _whatIsGround);

            if (_isGround)
            {
                _rigidbody.AddForce(Vector2.up * _jumpForce);
                // _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
            }
        }

        private void CheckDirectionMove()
        {
            if (_moveVelocity.x > 0)
            {
                transform.localScale = new Vector3(3, 3, 0);
            }
            else if (_moveVelocity.x < 0)
            {
                transform.localScale = new Vector3(-3, 3, 0);
            }
        }
    }
}
