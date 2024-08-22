using UnityEngine;

namespace Spells.Types
{
    public class EarthShield : Spell
    {
        [field: SerializeField] protected override float Lifetime { get; set; }

        private new void Start()
        {
            base.Start();
        }

        private new void Update()
        {
            base.Update();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log(collision);
        }
    }
}

