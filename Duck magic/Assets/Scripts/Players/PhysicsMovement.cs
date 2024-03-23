using UnityEngine;

namespace Players
{
    public class PhysicsMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;

        public void Move(Vector2 direction)
        {
            var offset = direction * (_speed * Time.deltaTime);
            _rigidbody.MovePosition(_rigidbody.position + offset);
        }
    }
}
