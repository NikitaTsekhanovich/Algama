using System;
using Interfaces;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Players
{
    public class SettingPlayerNetwork : MonoBehaviourPunCallbacks, IObserver
    {
        [SerializeField] private TMP_Text _playerName;
        private PhotonView _view;

        public PhotonView View => _view;

        [SerializeField] private Image _healthBar;
        
        private float _health;

        private void Awake()
        {
            _view = GetComponent<PhotonView>();
            _playerName.text = _view.Owner.NickName;
        }

        private void Start()
        {
            _health = _healthBar.fillAmount;
        }
    }
}
