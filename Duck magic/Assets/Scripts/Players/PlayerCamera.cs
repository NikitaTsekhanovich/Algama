using UnityEngine;
using Cinemachine;

namespace Players
{
    public class PlayerCamera : MonoBehaviour
    {
        private CinemachineVirtualCamera _cinemachineVirtual;

        public CinemachineVirtualCamera CinemachineVirtual => _cinemachineVirtual;

        private void Awake()
        {
            _cinemachineVirtual = 
                GameObject
                .FindGameObjectWithTag("VirtualCamera")
                .GetComponent<CinemachineVirtualCamera>();
        }
    }
}
