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
        private Animator _animator;

        public static Action<string> OnDeathHandler;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _currentSprite = GetComponent<SpriteRenderer>();
        }

        public void OnEnable()
        {
            HealthHandler.OnDiedPlayer += Died;
        }

        public void OnDisable()
        {
            HealthHandler.OnDiedPlayer -= Died;
        }

        private void Died(int currentId, string playerName)
        {
            if (_settingPlayerNetwork.View.InstantiationId == currentId)
            {
                _settingPlayerNetwork.View.RPC("SyncClearElements", RpcTarget.All);
                ChangeSprite();
                ChangeNickName();
                ChangeTag();
                ChangeLayer();
                OnDeathHandler?.Invoke(_settingPlayerNetwork.View.Owner.NickName);
            }
        }

        private void ChangeLayer()
        {
            gameObject.layer = LayerMask.NameToLayer("DeadPlayer");
        }

        private void ChangeSprite()
        {
            _animator.enabled = false;
            _currentSprite.sprite = _spriteDead;
            _currentSprite.color = new Color(255f, 255f, 255f, 0.9f);
        }

        private void ChangeNickName()
        {
            _nickName.text = $"Dead: {_nickName.text}";
        }

        private void ChangeTag()
        {
            transform.gameObject.tag = "DeadPlayer";
        }
    }
}
