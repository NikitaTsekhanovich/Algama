using Cinemachine;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class SpawnPlayers : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtual;
        [SerializeField] private float _minX, _minY, _maxX, _maxY;

        private void Start()
        {
            var randomPosition = new Vector3(Random.Range(_minX, _maxX), Random.Range(_minY, _maxY), 2);
            var newPLayer = PhotonNetwork.Instantiate(_player.name, randomPosition, Quaternion.identity);
            _cinemachineVirtual.Follow = newPLayer.transform;
        }
    }
}
