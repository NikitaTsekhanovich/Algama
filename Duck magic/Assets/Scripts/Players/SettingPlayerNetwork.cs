using Interfaces;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace Players
{
    public class SettingPlayerNetwork : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_Text _playerName;
        private PhotonView _view;

        public PhotonView View => _view;

        private void Awake()
        {
            _view = GetComponent<PhotonView>();
            _playerName.text = _view.Owner.NickName;
        }
    }
}
