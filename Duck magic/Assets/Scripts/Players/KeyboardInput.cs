using UnityEngine;

namespace Players
{
    public class KeyboardInput : MonoBehaviour
    {
        [SerializeField] private PhysicsMovement _movement;
        [SerializeField] private PlayerAttack _playerAttack;
        [SerializeField] private SettingPlayerNetwork _settingPlayerNetwork;
        [SerializeField] private HealthHandler _health;
        // [SerializeField] private PlayerAttack _playerAttack;
        [SerializeField] private Casting _casting;
        [SerializeField] private Spelling _spelling;

        private PhotonView _view;

        private void Start()
        {
            _view = GetComponent<PhotonView>();
        }

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
                    _spelling.CastSpell();
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _movement.Jump();
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    _casting.CastElement(MagickElementSource.Wind);
                }

                if (Input.GetKeyDown(KeyCode.W))
                {
                    _casting.CastElement(MagickElementSource.Lightning);
                }
                
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _casting.CastElement(MagickElementSource.Fire);
                }
                
                if (Input.GetKeyDown(KeyCode.S))
                {
                    _casting.CastElement(MagickElementSource.Shield);
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    _casting.CastElement(MagickElementSource.Earth);
                }
            }
        }

        private void FixedUpdate()
        {
            if (_settingPlayerNetwork.View.IsMine)
            {
                _movement.GroundCheck();
                _movement.ProcessInput();
            }
            else
            {
                _movement.SmoothMovement();
            }
        }
    }
}
