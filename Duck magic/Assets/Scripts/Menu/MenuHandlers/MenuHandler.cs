using System;
using UnityEngine;
using Photon.Pun;

namespace Menu.MenuHandlers
{
    public class MenuHandler : MonoBehaviourPunCallbacks
    {
        public static Action OnLoadLevel;

        public void StartGame()
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            OnLoadLevel?.Invoke();
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
