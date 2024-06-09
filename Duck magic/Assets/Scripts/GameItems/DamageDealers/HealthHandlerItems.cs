using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

namespace GameItems.DamageDealers.Dynamites
{
    public class HealthHandlerItems : MonoBehaviour
    {
        public static Action<PhotonView> OnDestroy;
        private float _health;

        public void Init(float health)
        {
            _health = health;
        }

        public void OnDamage(float damage, PhotonView view)
        {
            _health -= damage;

            if (_health <= 0)
                OnDestroy?.Invoke(view);
        }
    }
}