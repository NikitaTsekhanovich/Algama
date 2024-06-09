using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace GameItems.Properties
{
    public interface ICanDealDamage : ICanCheckPlayer
    {
        public void DealDamageTo<TPlayerHealth>(TPlayerHealth healthHandler, PhotonView view)
            where TPlayerHealth : Players.HealthHandler;
    }
}

