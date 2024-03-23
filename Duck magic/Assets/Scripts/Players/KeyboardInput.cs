using UnityEngine;

namespace Players
{
    public class KeyboardInput : MonoBehaviour
    {
        [SerializeField] private PhysicsMovement _movement;
        [SerializeField] private KeyCode _right;
        [SerializeField] private KeyCode _left;
        [SerializeField] private KeyCode _jump;
        [SerializeField] private KeyCode _attack;

        
        private void Update()
        {
            if (Input.GetKey(_right))
            {
                _movement.MoveRight();
            }
            else if (Input.GetKey(_left))
            {
                _movement.MoveLeft();
            }

            if (Input.GetKeyDown(_jump))
            {
                _movement.Jump();
            }
        }
    }
}
