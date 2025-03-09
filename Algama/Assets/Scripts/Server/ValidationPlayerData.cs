using UnityEngine;
using Photon.Pun;

namespace Server
{
    public class ValidationPlayerData : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject _errorWindow;
        
        public bool IsCorrectPlayerName(string playerName)
        {
            if (playerName.Length != 0 && playerName.Length <= 16)
            {
                PlayerPrefs.SetString("name", playerName);
                PhotonNetwork.NickName = playerName;
                return true;
            }
            
            _errorWindow.SetActive(true);
            return false;
        }
    }
}
