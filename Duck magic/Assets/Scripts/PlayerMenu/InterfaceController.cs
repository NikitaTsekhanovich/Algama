using GameLogic.LevelHandlers;
using Interfaces;
using Spawners;
using UnityEngine;

namespace PlayerMenu
{
    public class InterfaceController : MonoBehaviour, IObserver
    {
        [SerializeField] private GameObject _interface;
        
        public void OnEnable()
        {
            SpawnerPlayers.OnPlayerInterface += OnInterface;
            LevelLoader.OffPlayerInterface += OffInterface;
            PlayerMenuHandler.OffPlayerInterface += OffInterface;
        }

        public void OnDisable()
        {
            SpawnerPlayers.OnPlayerInterface -= OnInterface;
            LevelLoader.OffPlayerInterface -= OffInterface;
            PlayerMenuHandler.OffPlayerInterface -= OffInterface;
        }

        private void OnInterface()
        {
            _interface.SetActive(true);
        }

        private void OffInterface()
        {
            _interface.SetActive(false);
        }
    }
}
