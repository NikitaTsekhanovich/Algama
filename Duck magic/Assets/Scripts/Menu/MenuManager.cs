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

        public void CreateRoom()
        {
            var roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;
            PhotonNetwork.CreateRoom(_createInput.text, roomOptions);
            // PhotonNetwork.CreateRoom("Room", roomOptions, TypedLobby.Default);
        }

        public void JoinRoom()
        {
            PhotonNetwork.JoinRoom(_joinInput.text);
            // PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }
}
