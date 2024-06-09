using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Photon.Pun;

namespace GameItems.SpawnerItems
{
    public class ItemsFactory<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        [SerializeField] private T _prefab;
        [SerializeField] private List<Transform> _pointsToSpawn = new();

        public void InstanceItems()
        {
            foreach (var point in _pointsToSpawn)
            {
                var pos = new Vector3(point.position.x, point.position.y, 0);
                PhotonNetwork.Instantiate(_prefab.name, pos, Quaternion.identity);
            }
        }
    }
}
