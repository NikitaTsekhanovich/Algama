using UnityEngine;

namespace Players
{
    public class KeyboardInput : MonoBehaviour
    {
        [SerializeField] private PhysicsMovement _movement;
        
        private void Update()
        {
            var horizontal = Input.GetAxis(Axis.Horizontal);

            _movement.Move(new Vector2(horizontal, 0));
        }
    }
}
