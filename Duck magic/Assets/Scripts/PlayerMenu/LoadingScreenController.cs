using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace PlayerMenu
{
    public class LoadingScreenController : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private TextMeshProUGUI _progressText;
        private Image _background;
        
        public static LoadingScreenController instance;

        private void Start()
        {
            instance = this;
            _background = _loadingScreen.GetComponent<Image>();
        }
        
        public void StartAnimationFade()
        {
            // gameObject.GetComponent<Image>().raycastTarget = true;
            _loadingScreen.SetActive(true);

            DOTween.Sequence()
                .Append(_background.DOFade(1f, 0.5f));
        }
        
        public void EndAnimationFade()
        {
            DOTween.Sequence()
                .Append(_background.DOFade(0f, 0.5f));
            
            _loadingScreen.SetActive(false);
        }
    }
}
