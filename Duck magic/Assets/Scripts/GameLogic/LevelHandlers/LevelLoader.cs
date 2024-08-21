using System;
using System.Collections;
using GameLogic.PlayerDataControllers;
using Menu.MenuHandlers;
using Menu.Services;
using Photon.Pun;
using PlayerMenu;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLogic.LevelHandlers
{
    public class LevelLoader : Levels
    {
        public static Action OnPlayersScore;
        public static Action OffPlayersScore;
        public static Action OffPlayerInterface;
        public static Action OnClearPlayersScore;
        public static Action OnMenuMusic;
        public static Action OffMenuMusic;
        public static Action OnGameMusic;
        public static Action OffGameMusic;

        public void OnEnable()
        {
            MenuHandler.OnLoadLevel += StartGame;
            PlayerDeathController.OnEndLevel += EndLevel;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void OnDisable()
        {
            MenuHandler.OnLoadLevel -= StartGame;
            PlayerDeathController.OnEndLevel -= EndLevel;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            Debug.Log("Scene loaded!");
            if (scene.name != "LoadingScene")
            {
                LoadingScreenController.Instance.EndAnimationFade();
                
                if (scene.name != "Menu")
                {
                    OffMenuMusic?.Invoke();
                    OnGameMusic?.Invoke();
                }
                else
                {
                    OffGameMusic?.Invoke();
                    OnMenuMusic?.Invoke();
                }
            }
        }

        private void StartGame()
        {
            CreateLevelsQueue();
            StartLevel();
        }
        
        [PunRPC]
        private void StartLoadingScreen()
        {
            LoadingScreenController.Instance.StartAnimationFade();
        }

        [PunRPC]
        private void EndGame()
        {
            StartCoroutine(ReturnLobby());
        }

        private IEnumerator ReturnLobby()
        {
            OnClearPlayersScore?.Invoke();
            OffPlayerInterface?.Invoke();
            base.photonView.RPC("StartLoadingScreen", RpcTarget.All);

            yield return new WaitForSeconds(1f);
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("Menu");
                PhotonNetwork.CurrentRoom.IsOpen = true;
            }
            
            yield return new WaitForSeconds(0.5f);
            RoomService.Instance.ReturnRoom();
        }

        private void StartLevel()
        {
            base.photonView.RPC("StartLoadingScreen", RpcTarget.All);
            // PhotonGarbageCollector.FindUnnecessaryObjects();

            var currentLevel = currentLevels.Dequeue();
            PhotonNetwork.LoadLevel(currentLevel);
        }

        private void EndLevel()
        {
            StartCoroutine(ReloadLevel());
        }

        private IEnumerator ReloadLevel()
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
                    base.photonView.RPC("EndGame", RpcTarget.All);
                }
            }
        }
    }
}
