using System;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace Server
{
    public class ConnectToServer : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
            SceneManager.LoadScene("Menu");
        }
    }
}
