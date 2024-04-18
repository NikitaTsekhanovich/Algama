using Menu.Validators;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Menu.Services
{
    public class RoomService : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_Text _roomTitle;
        
        [SerializeField] private GameObject _lobby;
        [SerializeField] private GameObject _room;
        
                
        [SerializeField] private TMP_InputField _createRoomInput;
        [SerializeField] private ValidationRoomData _validationRoomData;
        
        [SerializeField] private TMP_InputField _joinRoomInput;
        
        public static RoomService Instance;

        private void Awake()
        {
            Instance = this;
        }
        
        public void CreateRoom()
        {
            var roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;
          
            if (!_validationRoomData.IsCorrectRoomName(_createRoomInput.text))
            {
                _room.SetActive(false);
                return;
            }

            _roomTitle.text = _createRoomInput.text;
            PhotonNetwork.CreateRoom(_createRoomInput.text, roomOptions);
        }
        
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log(returnCode);
            Debug.Log(message);
        }
        
        public void JoinRoom(RoomInfo roomInfo)
        {
            _lobby.SetActive(false);
            _room.SetActive(true);
            _roomTitle.text = roomInfo.Name;
            PhotonNetwork.JoinRoom(roomInfo.Name);
        }
        
        public void JoinRoomId()
        {
            _lobby.SetActive(false);
            _room.SetActive(true);
            _roomTitle.text = _joinRoomInput.text;
            PhotonNetwork.JoinRoom(_joinRoomInput.text);
        }
        
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}
