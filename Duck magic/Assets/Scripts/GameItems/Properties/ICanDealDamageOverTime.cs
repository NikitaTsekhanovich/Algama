using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameItems.Properties
{
    public interface ICanDealDamageOverTime : ICanCheckPlayer
    {
        public float DamagePerSeocnd { get; }
        public IEnumerator OnStay();
    }
}

