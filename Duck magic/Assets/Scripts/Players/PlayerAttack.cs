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
            PhotonNetwork.Instantiate(_bullet.name, _throwPoint.position, _throwPoint.rotation);
        }
    }
}
