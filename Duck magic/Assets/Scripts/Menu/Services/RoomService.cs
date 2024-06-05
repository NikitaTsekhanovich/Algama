using Menu.Validators;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using System;

namespace Menu.Services
{
    public class RoomService : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_Text _roomTitle;
        
        [SerializeField] private GameObject _lobby;
        [SerializeField] private GameObject _room;
        [SerializeField] private GameObject _buttonsNavigate;
        [SerializeField] private GameObject _blockInput;

        [SerializeField] private TMP_InputField _createRoomInput;
        [SerializeField] private ValidationRoomData _validationRoomData;
        
        [SerializeField] private TMP_InputField _joinRoomInput;
        
        public static RoomService Instance;

        public static Action OnReturnRoom;

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
            Debug.Log($"Error create room: {returnCode}");
            Debug.Log($"Error create room: {message}");
            // _room.SetActive(false);
            // открыть кнопки и вывести ошибку, что комната уже создана
        }
        
        public override void OnCreatedRoom()
        {
            
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log($"Error join room: {returnCode}");
            Debug.Log($"Error join room: {message}");
        }
        
        public override void OnJoinedRoom()
        {
            _lobby.SetActive(false);
            _room.SetActive(true);
            _blockInput.SetActive(false);
        }
        
        public void JoinRoom(RoomInfo roomInfo)
        {
            _blockInput.SetActive(true);
            _roomTitle.text = roomInfo.Name;
            PhotonNetwork.JoinRoom(roomInfo.Name);
        }
        
        public void JoinRoomId()
        {
            _roomTitle.text = _joinRoomInput.text;
            PhotonNetwork.JoinRoom(_joinRoomInput.text);
        }
        
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public void ReturnRoom()
        {
            _buttonsNavigate.SetActive(false);
            _room.SetActive(true);
            OnReturnRoom?.Invoke();
        }
    }
}
