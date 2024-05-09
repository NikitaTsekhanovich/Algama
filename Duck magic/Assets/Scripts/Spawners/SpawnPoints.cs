using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    public class SpawnPoints : MonoBehaviour
    {
        private readonly List<Transform> _coordinates = new List<Transform>();

        public List<Transform> Coordinates => _coordinates;

        private void Awake()
        {
            var spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints");

            foreach (var spawnPoint in spawnPoints)
            {
                _coordinates.Add(spawnPoint.transform);
            }
        }
    }
}
