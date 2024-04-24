using UnityEngine;
using UnityEngine.Serialization;

namespace GameObjects.MagicStones
{
    [CreateAssetMenu(fileName = "HealerStoneInfo", menuName = "Magic stones/ Healer stone")]
    public class HealerStoneInfo : ScriptableObject
    {
        [SerializeField] private float _healPerSecond;
        [SerializeField] private MagicStoneTypes _magicStoneType;
        [SerializeField] private string _name;

        public float HealPerSecond => _healPerSecond;
        public MagicStoneTypes MagicStoneType => _magicStoneType;
        public string Name => _name;
    }
}
