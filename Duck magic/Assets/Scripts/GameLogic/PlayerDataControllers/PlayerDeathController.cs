using System;
using GameLogic.LevelHandlers;
using Interfaces;
using Photon.Pun;
using PlayerMenu;
using Players;
using UnityEngine;

namespace GameLogic.PlayerDataControllers
{
    public class PlayerDeathController : MonoBehaviour, IObserver
    {
        private int _currentNumberPlayers;

        public static Action OnEndLevel;
        public static Action<string, int> OnRecordPlayerScore;

        private void Awake()
        {
            _currentNumberPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
        }
        
        public void OnEnable()
        {
            DeathState.OnDeathHandler += DiedPlayer;
            PlayerMenuHandler.OnPlayerDisconnect += UpdateNumberPlayers;
        }

        public void OnDisable()
        {
            DeathState.OnDeathHandler -= DiedPlayer;
            PlayerMenuHandler.OnPlayerDisconnect += UpdateNumberPlayers;
        }



        private void UpdateNumberPlayers()
        {
            _currentNumberPlayers -= 1;
            Debug.Log(_currentNumberPlayers);
        }

        private void DiedPlayer(string playerName)
        {
            OnRecordPlayerScore?.Invoke(
                playerName, 
                PhotonNetwork.CurrentRoom.PlayerCount - _currentNumberPlayers);

            UpdateNumberPlayers();

            if (_currentNumberPlayers <= 1)
            {
                KillLastPlayer();
            }
        }

        private void KillLastPlayer()
        {
            var nickNameLastPlayer = GameObject
                .FindGameObjectWithTag("Player")
                .GetComponent<PhotonView>()
                .Owner
                .NickName;
            
            OnRecordPlayerScore?.Invoke(
                nickNameLastPlayer, 
                PhotonNetwork.CurrentRoom.PlayerCount - _currentNumberPlayers);

            OnEndLevel?.Invoke();
        }
    }
}
