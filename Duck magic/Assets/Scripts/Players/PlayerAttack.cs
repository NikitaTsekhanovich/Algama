using UnityEngine;
using Photon.Pun;

namespace Players
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _throwPointRight;
        [SerializeField] private Transform _throwPointLeft;
        [SerializeField] private PhysicsMovement _physicsMovement;

        public void Attack()
        {
            if (_physicsMovement.SpriteRendererPlayer.flipX)
            {
                var newMagicBall = PhotonNetwork.Instantiate(_bullet.name, _throwPointLeft.position, _throwPointRight.rotation);
                newMagicBall.GetComponent<PhotonView>().RPC("OnChangeDirection", RpcTarget.AllBuffered);
            }
            else
            {
                PhotonNetwork.Instantiate(_bullet.name, _throwPointRight.position, _throwPointRight.rotation);
            }
        }
    }
}
