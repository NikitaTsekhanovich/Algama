using UnityEngine;
using Photon.Pun;

namespace Players
{
    public class KeyboardInput : MonoBehaviour
    {
        [SerializeField] private PhysicsMovement _movement;
        [SerializeField] private PlayerAttack _playerAttack;
        [SerializeField] private SettingPlayerNetwork _settingPlayerNetwork;

        private void Update()
        {
            if (_settingPlayerNetwork.View.IsMine)
            {
                var horizontalInput = Input.GetAxis(Axis.Horizontal);
                _movement.Move(horizontalInput);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    _playerAttack.Attack();
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _movement.Jump();
                }
            }
        }
    }
}
