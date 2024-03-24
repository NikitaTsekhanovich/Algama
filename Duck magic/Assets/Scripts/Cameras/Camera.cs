using UnityEngine;

namespace Cameras
{
    public class Camera : MonoBehaviour
    {
        [SerializeField] private Transform _player;
            
        private void Update()
        {
            var cameraPosition = transform.position;
            cameraPosition.x = _player.position.x;
            cameraPosition.y = _player.position.y;

            transform.position = cameraPosition;
        }
    }
}
