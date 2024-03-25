using System;
using UnityEngine;
using Photon.Pun;

namespace Players
{
    public class KeyboardInput : MonoBehaviour
    {
        [SerializeField] private PhysicsMovement _movement;
        [SerializeField] private KeyCode _right;
        [SerializeField] private KeyCode _left;
        [SerializeField] private KeyCode _jump;
        [SerializeField] private KeyCode _attack;
        
        private PhotonView _view;

        private void Start()
        {
            _view = GetComponent<PhotonView>();
        }

        private void Update()
        {
            if (_view.IsMine)
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
}
