using System;
using System.Collections;
using GameLogic.PlayerDataControllers;
using Interfaces;
using Menu.MenuHandlers;
using Menu.Services;
using Photon.Pun;
using Players;
using UnityEngine;

namespace GameLogic.LevelHandlers
{
    public class LevelLoader : Levels, IObserver
    {
        public static Action OnPlayersScore;
        public static Action OffPlayersScore;
        public static Action OffPlayerInterface;
        public static Action OnClearPlayersScore;

        public void OnEnable()
        {
            MenuHandler.OnLoadLevel += StartGame;
            PlayerDeathController.OnEndLevel += EndLevel;
            DeathState.OnPLayersDied += EndGame;
        }

        public void OnDisable()
        {
            MenuHandler.OnLoadLevel -= StartGame;
            PlayerDeathController.OnEndLevel -= EndLevel;
            DeathState.OnPLayersDied -= EndGame;
        }

        private void StartGame()
        {
            CreateLevelsQueue();
            StartLevel();
        }

        private void EndGame()
        {
            StartCoroutine(ReturnLobby());
        }

        private IEnumerator ReturnLobby()
        {
            OnClearPlayersScore?.Invoke();
            OffPlayerInterface?.Invoke();
            
            PhotonNetwork.LoadLevel("Menu");
            PhotonNetwork.CurrentRoom.IsOpen = true;
            
            yield return new WaitForSeconds(1f);
            RoomService.Instance.ReturnRoom();
        }

        private void StartLevel()
        {
            var currentLevel = currentLevels.Dequeue();
            PhotonNetwork.LoadLevel(currentLevel);
        }

        private void EndLevel(PhotonView view)
        {
            StartCoroutine(ReloadLevel(view));
        }

        private IEnumerator ReloadLevel(PhotonView view)
        {
            OnPlayersScore?.Invoke();
            yield return new WaitForSeconds(5f);
            OffPlayersScore?.Invoke();

            if (PhotonNetwork.IsMasterClient)
            {
                if (currentLevels.Count >= 1)
                {
                    StartLevel();
                }
                else
                {
                    view.RPC("DiedPlayersLastLevel", RpcTarget.All);
                }
            }
        }
    }
}
