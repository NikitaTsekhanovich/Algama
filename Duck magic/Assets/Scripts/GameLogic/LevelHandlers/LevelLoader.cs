using System;
using System.Collections;
using GameLogic.PlayerDataHandlers;
using Interfaces;
using Menu.MenuHandlers;
using Photon.Pun;
using UnityEngine;

namespace GameLogic.LevelHandlers
{
    public class LevelLoader : Levels, IObserver
    {
        public static Action OnPlayersScore;
        public static Action OffPlayersScore;

        public void OnEnable()
        {
            MenuHandler.OnLoadLevel += StartLevel;
            PlayerDeathHandler.OnEndLevel += EndLevel;
        }

        public void OnDisable()
        {
            MenuHandler.OnLoadLevel -= StartLevel;
            PlayerDeathHandler.OnEndLevel -= EndLevel;
        }
        
        private void StartLevel()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                var currentLevel = currentLevels.Dequeue();
                PhotonNetwork.LoadLevel(currentLevel);
            }
        }

        private void EndLevel()
        {
            if (currentLevels.Count >= 1)
            {
                StartCoroutine(ReloadLevel());
            }
            else
            {
                OnPlayersScore?.Invoke();
                // желательно выйти в лобби 
                // или выкинуть всех в меню 
            }
        }

        private IEnumerator ReloadLevel()
        {
            OnPlayersScore?.Invoke();
            
            yield return new WaitForSeconds(6f);
            
            OffPlayersScore?.Invoke();
            StartLevel();
        }
    }
}
