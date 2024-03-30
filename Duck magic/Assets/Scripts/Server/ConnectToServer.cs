using System;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

namespace Server
{
    public class ConnectToServer : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_InputField _playerNameInput;
        [SerializeField] private TMP_Text _buttonText;

        private void Start()
        {
            _playerNameInput.text = PlayerPrefs.GetString("name");
            PhotonNetwork.NickName = _playerNameInput.text;
        }

        public void OnClickConnect()
        {
            CheckNameInput(_playerNameInput.text.Length != 0);
            _buttonText.text = "Connecting...";
            PhotonNetwork.ConnectUsingSettings();
        }
        
        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
            SceneManager.LoadScene("Menu");
        }

        private void CheckNameInput(bool isCorrectInput)
        {
            if (isCorrectInput)
            {
                PlayerPrefs.SetString("name", _playerNameInput.text);
                PhotonNetwork.NickName = _playerNameInput.text;
            }
            else
            {
                PhotonNetwork.NickName = "Anonymous 228";
            }
        }
    }
}
