using UnityEngine;
using Photon.Pun;

namespace Players
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _throwPoint;
        
        public void Attack()
        {
            var newMagicBall = PhotonNetwork.Instantiate(_bullet.name, _throwPoint.position, _throwPoint.rotation);
            
            newMagicBall.transform.localScale = new Vector3(
                0.04f * Mathf.Sign(transform.localScale.x), 0.3f, 0);
        }
    }
}
