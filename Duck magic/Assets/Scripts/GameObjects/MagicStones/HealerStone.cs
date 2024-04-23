using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace GameObjects.MagicStones
{
    public class HealerStone : MonoBehaviour
    {
        [SerializeField] private HealerStoneInfo _healerStoneInfo;
        private int _numberPlayers;
        
        public static Action<float, PhotonView> OnHealPlayer;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player") && other.GetComponent<PhotonView>().IsMine)
            {
                _numberPlayers += 1;
                StartCoroutine(OnStay(other.GetComponent<PhotonView>()));
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player") && other.GetComponent<PhotonView>().IsMine)
            {
                _numberPlayers -= 1;
            }
        }

        private IEnumerator OnStay(PhotonView playerView)
        {
            while (_numberPlayers > 0)
            {
                OnHealPlayer?.Invoke(_healerStoneInfo.HealPerSecond, playerView);
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
