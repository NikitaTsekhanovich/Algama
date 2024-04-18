using UnityEngine;
using Photon.Pun;

namespace Menu.MenuHandlers
{
    public class MenuHandler : MonoBehaviourPunCallbacks
    {
        public void StartGame()
        {
            PhotonNetwork.LoadLevel("Game");
        }
        
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
