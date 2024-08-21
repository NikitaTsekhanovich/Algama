using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace PlayerMenu
{
    public class LoadingScreenController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _loadingText;
        [SerializeField] private Image _background;
        
        public static LoadingScreenController Instance;

        private void Start() 
        {             
            if (Instance == null) 
                Instance = this; 
            else if (Instance == this)
                Destroy(gameObject); 
        }

        public void StartAnimationFade()
        {
            _loadingText.DOFade(1f, 0.4f);

            DOTween.Sequence()
                .Append(_background.DOFade(1f, 0.4f));
        }

        public void EndAnimationFade()
        {
            _loadingText.DOFade(0f, 0.4f);

            DOTween.Sequence()
                .Append(_background.DOFade(0f, 0.4f));
        }
    }
}
