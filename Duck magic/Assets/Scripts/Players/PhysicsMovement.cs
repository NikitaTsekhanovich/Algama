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

        public SpriteRenderer SpriteRendererPlayer => _spriteRenderer;
        
        private void Start()
        {
            PhotonNetwork.SendRate = 20;
            PhotonNetwork.SerializationRate = 15;
            _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        }

        public void ProcessInput()
        {
            var move = new Vector3(Input.GetAxisRaw(Axis.Horizontal), 0);
            transform.position += move * _speed * Time.deltaTime;
        }

        public void SmoothMovement()
        {
            transform.position = Vector3.Lerp(transform.position, _smooth, Time.deltaTime * 10);
        }

        public void Jump()
        {
            _isGround = Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckRadius, _whatIsGround);

            if (_isGround)
            {
                _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                // _rigidbody.velocity = Vector2.up * _jumpForce;
            }
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
