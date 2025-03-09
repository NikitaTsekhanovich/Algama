using UnityEngine;

namespace Spells.Properties
{
    public interface ICanMove
    {
        public float MoveSpeed { get; set; }
        public Vector2 Direction { get; }
    }
}