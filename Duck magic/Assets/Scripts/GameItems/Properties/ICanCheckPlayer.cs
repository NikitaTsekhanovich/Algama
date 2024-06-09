using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameItems.Properties
{
    public interface ICanCheckPlayer
    {
        public void OnTriggerEnter2D(Collider2D other);
        public void OnTriggerExit2D(Collider2D other);
    }
}

