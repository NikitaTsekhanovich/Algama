using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

namespace Menu
{
    public class MenuManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_InputField _createInput;
        [SerializeField] private TMP_InputField _joinInput;
        [SerializeField] private TMP_InputField _playerNameInput;

        private void Start()
        {
            _playerNameInput.text = PlayerPrefs.GetString("name");
            PhotonNetwork.NickName = _playerNameInput.text;
        }

        public void CreateRoom()
        {
            var roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;
            PhotonNetwork.CreateRoom(_createInput.text, roomOptions);
        }

        public void SaveName()
        {
            PlayerPrefs.SetString("name", _playerNameInput.text);
            PhotonNetwork.NickName = _playerNameInput.text;
        }

        public void JoinRoom()
        {
            PhotonNetwork.JoinRoom(_joinInput.text);
        }

        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }
}
