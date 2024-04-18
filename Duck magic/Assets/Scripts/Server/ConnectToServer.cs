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
        [SerializeField] private ValidationPlayerData _validationPlayerData;

        private void Start()
        {
            _playerNameInput.text = PlayerPrefs.GetString("name");
            PhotonNetwork.NickName = _playerNameInput.text;
        }

        public void OnClickConnect()
        {
            var isCorrectPlayerName = _validationPlayerData.IsCorrectPlayerName(_playerNameInput.text);

            if (isCorrectPlayerName)
            {
                _buttonText.text = "Connecting...";
                PhotonNetwork.ConnectUsingSettings();
            }
        }
        
        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
            PhotonNetwork.AutomaticallySyncScene = true;
            SceneManager.LoadScene("Menu");
        }
    }
}
