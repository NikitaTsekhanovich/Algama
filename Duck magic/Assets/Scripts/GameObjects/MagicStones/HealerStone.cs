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
        
        public static Action<float, int> OnTreatmentPlayer;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player") && other.GetComponent<PhotonView>().IsMine)
            {
                _numberPlayers += 1;
                StartCoroutine(OnStay(other.GetComponent<PhotonView>().InstantiationId));
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player") && other.GetComponent<PhotonView>().IsMine)
            {
                _numberPlayers -= 1;
            }
        }

        private IEnumerator OnStay(int playerId)
        {
            while (_numberPlayers > 0)
            {
                OnTreatmentPlayer?.Invoke(_healerStoneInfo.HealPerSecond, playerId);
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
