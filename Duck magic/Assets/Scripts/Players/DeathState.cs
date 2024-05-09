using System;
using Interfaces;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace Players
{
    public class DeathState : MonoBehaviour, IObserver
    {
        [SerializeField] private Sprite _spriteDead;
        [SerializeField] private TMP_Text _nickName;
        [SerializeField] private SettingPlayerNetwork _settingPlayerNetwork;

        private SpriteRenderer _currentSprite;
        
        public static Action OnPLayersDied;
        public static Action<string, PhotonView> OnDeathHandler;

        private void Start()
        {
            _currentSprite = gameObject.GetComponent<SpriteRenderer>();
        }

        public void OnEnable()
        {
            HealthHandler.OnDiedPlayer += Died;
        }

        public void OnDisable()
        {
            HealthHandler.OnDiedPlayer -= Died;
        }

        private void Died(int currentId, string playerName, PhotonView view)
        {
            if (_settingPlayerNetwork.View.InstantiationId == currentId)
            {
                ChangeSprite();
                ChangeNickName();
                ChangeTag();
                OnDeathHandler?.Invoke(_settingPlayerNetwork.View.Owner.NickName, _settingPlayerNetwork.View);
            }
        }

        private void ChangeSprite()
        {
            _currentSprite.sprite = _spriteDead;
            _currentSprite.color = new Color(255f, 255f, 255f, 0.9f);
        }

        private void ChangeNickName()
        {
            _nickName.text = $"Dead: {_nickName.text}";
        }

        private void ChangeTag()
        {
            transform.gameObject.tag = "Field";
        }
        
        [PunRPC]
        private void DiedPlayersLastLevel()
        {
            OnPLayersDied?.Invoke();
        }
    }
}
