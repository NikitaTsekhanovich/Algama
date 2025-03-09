using Photon.Pun;
using Players;
using UnityEngine;
using System;

namespace Spawners
{
    public class SpawnerPlayers : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private SpawnPoints _spawnPoints;
        
        public static Action OnPlayerInterface;

        private void Start()
        {
            var currentCoordinates = _spawnPoints.Coordinates[
                PhotonNetwork.LocalPlayer.ActorNumber % 
                _spawnPoints.Coordinates.Count];
            
            var position = new Vector3(
                currentCoordinates.position.x, 
                currentCoordinates.position.y, 2);
            
            var newPLayer = PhotonNetwork.Instantiate(_player.name, position, Quaternion.identity);
            
            newPLayer.GetComponent<PlayerCamera>().CinemachineVirtual.Follow = newPLayer.transform;
            OnPlayerInterface?.Invoke();
        }
    }
}
