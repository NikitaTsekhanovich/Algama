using System;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class SpawnPlayers : MonoBehaviour
    {
        public GameObject player;
        public float minX, minY, maxX, maxY;

        private void Start()
        {
            var randomPosition = new Vector3(Random.Range(minX, minY), Random.Range(maxX, maxY), 2);
            PhotonNetwork.Instantiate(player.name, randomPosition, Quaternion.identity);
        }
    }
}
