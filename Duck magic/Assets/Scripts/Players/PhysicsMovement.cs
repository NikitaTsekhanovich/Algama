using Photon.Pun;
using UnityEngine;

namespace Players
{
    public class PhysicsMovement : MonoBehaviourPun, IPunObservable
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;

        [SerializeField] private Transform _groundCheckPoint;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private LayerMask _whatIsGround;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        private bool _isGround;
        private Vector3 _smooth;
        private Rigidbody2D _rigidbody;
        private Animator _animator;

        private static readonly int MoveX = Animator.StringToHash("moveX");
        private static readonly int MoveY = Animator.StringToHash("moveY");
        private static readonly int IsJumping = Animator.StringToHash("isJumping");

        public SpriteRenderer SpriteRendererPlayer => _spriteRenderer;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            PhotonNetwork.SendRate = 20;
            PhotonNetwork.SerializationRate = 15;
        }

        public void ProcessInput()
        {
            var move = new Vector3(Input.GetAxisRaw(Axis.Horizontal), 0);
            
            if (move.x != 0)
                transform.position += move * _speed * Time.deltaTime;
            else
                _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);

            _animator.SetFloat(MoveX, Mathf.Abs(move.x));
        }

        public void SmoothMovement()
        {
            transform.position = Vector3.Lerp(transform.position, _smooth, Time.deltaTime * 10);
        }

        public void GroundCheck()
        {
            _animator.SetFloat(MoveY, _rigidbody.velocity.y);
            _isGround = Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckRadius, _whatIsGround);
            _animator.SetBool(IsJumping, !_isGround);
        }

        public void Jump()
        {
            if (!_isGround) return;
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _animator.SetBool(IsJumping, true);
        }

        public void CheckDirectionMove(bool isLeft, PhotonView view)
        {
            _spriteRenderer.flipX = isLeft;
            view.RPC("OnDirectionChange", RpcTarget.AllBuffered, isLeft);
        }

        [PunRPC]
        private void OnDirectionChange(bool isRight)
        {
            _spriteRenderer.flipX = isRight;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(transform.position);
            }
            else if (stream.IsReading)
            {
                _smooth = (Vector3)stream.ReceiveNext();
            }
        }
    }
}