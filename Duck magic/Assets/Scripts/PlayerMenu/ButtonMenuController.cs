using GameLogic.LevelHandlers;
using Interfaces;
using Spawners;
using UnityEngine;

namespace PlayerMenu
{
    public class ButtonMenuController : MonoBehaviour, IObserver
    {
        [SerializeField] private GameObject _menuButton;
        
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
            _menuButton.SetActive(true);
        }

        private void OffInterface()
        {
            _menuButton.SetActive(false);
        }
    }
}
