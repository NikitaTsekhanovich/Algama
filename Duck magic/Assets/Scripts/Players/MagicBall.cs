using System;
using Photon.Pun;
using UnityEngine;

namespace Players
{
    public class MagicBall : MonoBehaviourPun
    {
        [SerializeField] private float _magicBallSpeed;
        [SerializeField] private float _damage;
        
        private bool _isLeftDirection;
        
        public static Action<float, PhotonView> OnDamagePlayer;

        private void Update()
        {
            if (!_isLeftDirection)
            {
                transform.Translate(Vector2.right * Time.deltaTime * _magicBallSpeed);
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * _magicBallSpeed);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                OnDamagePlayer?.Invoke(_damage, other.GetComponent<PhotonView>());
            }

            if (!other.gameObject.CompareTag("Field"))
            {
                Destroy(gameObject);
            }
        }

        [PunRPC]
        private void OnChangeDirection()
        {
            _isLeftDirection = true;
        }
    }
}
