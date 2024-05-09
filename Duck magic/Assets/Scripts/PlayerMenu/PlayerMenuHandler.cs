using System;
using System.Collections;
using Menu.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerMenu
{
    public class PlayerMenuHandler : MonoBehaviour
    {
        public static Action OffPlayerInterface;
        public static Action OnPlayerDisconnect;
        
        public void BackToMenu()
        {
            OnPlayerDisconnect?.Invoke();
            StartCoroutine(LoadMenuScene());
        }

        private IEnumerator LoadMenuScene()
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Menu");
            yield return new WaitForSeconds(2f);
            RoomService.Instance.LeaveRoom();
            OffPlayerInterface?.Invoke();
        }
    }
}
