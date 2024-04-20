using Unity.VisualScripting;
using UnityEngine;

namespace GameObjects.MagicStones
{
    public class HealerStone : MonoBehaviour
    {
        [SerializeField] private HealerStoneInfo _healerStoneInfo;
        
        private void OnTriggerStay2D(Collider2D other)
        {
            Debug.Log("AAA");
            Debug.Log(_healerStoneInfo.HealPerSecond);
        }
    }
}
