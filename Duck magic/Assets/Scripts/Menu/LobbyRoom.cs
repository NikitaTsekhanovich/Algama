using UnityEngine;
using Photon.Realtime;
using TMPro;

namespace Menu
{
    public class LobbyRoom : MonoBehaviour
    {
        [SerializeField] private TMP_Text _roomText;
        [SerializeField] private TMP_Text _countPlayersText;

        public void SetInfo(RoomInfo roomInfo)
        {
            _roomText.text = $"Room name: {roomInfo.Name}";
            _countPlayersText.text = $"{roomInfo.PlayerCount}/{roomInfo.MaxPlayers}";
        }
    }
}
