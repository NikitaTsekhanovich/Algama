using UnityEngine;

namespace Players
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _throwPoint;
        
        public void Attack()
        {
            Instantiate(_bullet, _throwPoint.position, _throwPoint.rotation);
        }
    }
}
