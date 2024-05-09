using UnityEngine;

namespace Players
{
    public class KeyboardInput : MonoBehaviour
    {
        [SerializeField] private PhysicsMovement _movement;
        [SerializeField] private PlayerAttack _playerAttack;
        [SerializeField] private SettingPlayerNetwork _settingPlayerNetwork;
        [SerializeField] private HealthHandler _health;

        private void Update()
        {
            if (_settingPlayerNetwork.View.IsMine)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    _movement.CheckDirectionMove(false, _settingPlayerNetwork.View);
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    _movement.CheckDirectionMove(true, _settingPlayerNetwork.View);
                }
                
                if (Input.GetKeyDown(KeyCode.F) && _health.IsAlive)
                {
                    _playerAttack.Attack();
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _movement.Jump();
                }
            }
        }

        private void FixedUpdate()
        {
            if (_settingPlayerNetwork.View.IsMine)
            {
                _movement.ProcessInput();
            }
            else
            {
                _movement.SmoothMovement();
            }
        }
    }
}
