using System;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace Players
{
    public class PlayerSettings : MonoBehaviourPun
    {
        [SerializeField] private TMP_Text _playerName;
        
        private PhotonView _view;

        private void Start()
        {
            _view = GetComponent<PhotonView>();

            _playerName.text = _view.Owner.NickName;
        }
    }
}
