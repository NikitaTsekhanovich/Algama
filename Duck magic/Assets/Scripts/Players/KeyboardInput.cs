using System;
using UnityEngine;
using Photon.Pun;

namespace Players
{
    public class KeyboardInput : MonoBehaviour
    {
        [SerializeField] private PhysicsMovement _movement;
        [SerializeField] private PlayerAttack _playerAttack;

        private PhotonView _view;

        private void Start()
        {
            _view = GetComponent<PhotonView>();
        }

        private void FixedUpdate()
        {
            if (_view.IsMine)
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
