using System;
using Photon.Pun;

namespace GameLogic
{
    public class GameSystem : MonoBehaviourPunCallbacks
    {
        private int _currentNumberPlayers;

        public static Action OnEndLevel;

        private void Awake()
        {
            _currentNumberPlayers = PhotonNetwork.CountOfPlayers;
        }

        private void DiedPlayer()
        {
            _currentNumberPlayers -= 1;

            if (_currentNumberPlayers <= 0)
            {
                OnEndLevel?.Invoke();
            }
        }
    }
}
