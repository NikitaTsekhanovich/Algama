using System;
using System.Collections;
using Interfaces;
using Photon.Pun;
using Players;
using UnityEngine;

namespace GameLogic.PlayerDataHandlers
{
    public class PlayerDeathHandler : MonoBehaviourPunCallbacks, IObserver
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
            HealthHandler.OnDied += DiedPlayer;
        }

        public void OnDisable()
        {
            HealthHandler.OnDied -= DiedPlayer;
        }
        
        private void DiedPlayer(string playerName)
        {
            OnRecordPlayerScore?.Invoke(
                playerName, 
                PhotonNetwork.CurrentRoom.PlayerCount - _currentNumberPlayers);
            
            _currentNumberPlayers -= 1;
            
            if (_currentNumberPlayers <= 1)
            {
                StartCoroutine(KillLastPlayer());
            }
        }

        private IEnumerator KillLastPlayer()
        {
            yield return new WaitForSeconds(1f);
            
            var lastPlayer = GameObject.FindGameObjectWithTag("Player");
            
            OnRecordPlayerScore?.Invoke(
                lastPlayer.GetComponent<PhotonView>().Owner.NickName, 
                PhotonNetwork.CurrentRoom.PlayerCount - _currentNumberPlayers);
                
            Destroy(lastPlayer.gameObject);
                
            OnEndLevel?.Invoke();
        }
    }
}
