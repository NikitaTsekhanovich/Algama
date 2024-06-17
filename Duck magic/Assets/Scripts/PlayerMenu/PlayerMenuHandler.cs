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
        public static Action OnMenuMusic;
        public static Action OffGameMusic;
        
        public void BackToMenu()
        {
            OnPlayerDisconnect?.Invoke();
            StartCoroutine(LoadMenuScene());
        }

        private IEnumerator LoadMenuScene()
        {
            OffGameMusic?.Invoke();
            LoadingScreenController.Instance.StartAnimationFade();
            OffPlayerInterface?.Invoke();
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Menu");
            yield return new WaitForSeconds(1f);
            RoomService.Instance.LeaveRoom();
            OnMenuMusic?.Invoke();
        }
    }
}
