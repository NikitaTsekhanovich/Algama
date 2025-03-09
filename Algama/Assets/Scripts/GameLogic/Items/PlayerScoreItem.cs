using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace GameLogic.Items
{
    public class PlayerScoreItem : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_Text _playerNameText;
        [SerializeField] private TMP_Text _playerScoreText;

        public void SetInfo(string name, int score)
        {
            _playerNameText.text = name;
            _playerScoreText.text = $"{score}";
        }
    }
}
