using System.Collections;
using System.Collections.Generic;
using GameItems.DamageDealers.Dynamites;
using Photon.Pun;
using UnityEngine;

namespace GameItems.SpawnerItems
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private DynamiteFactory _dynamiteFactory;

        private void Start()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                InstanceDynamite();
            }
        }

        private void InstanceDynamite()
        {
            _dynamiteFactory.InstanceItems();
        }
    }
}

