using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Menu
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Dropdown Dropdown;
        [SerializeField] private Toggle Toggle;
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundSlider;
        private List<Resolution> Resolutions;
        private string _musicMixerName = "MusicVol";
        private string _soundMixerName = "SoundVol";

        private const float _multiplier = 20f;

        private void Awake()
        {
            _musicSlider.onValueChanged.AddListener(HandleMusicSlider);
            _soundSlider.onValueChanged.AddListener(HandleSoundSlider);
        }

        private void HandleMusicSlider(float value)
        {
            var volume = Mathf.Log10(value) * _multiplier;
            _mixer.SetFloat(_musicMixerName, volume);
        }

        private void HandleSoundSlider(float value)
        {
            var volume = Mathf.Log10(value) * _multiplier;
            _mixer.SetFloat(_soundMixerName, volume);
        }

        private void Start()
        {
            Toggle.isOn = Screen.fullScreen;
            Resolutions = Screen.resolutions.Distinct().ToList();
            Resolutions.Reverse();
            var res = Resolutions.Select(resolution => resolution.ToString()).ToList();
            Dropdown.ClearOptions();
            Dropdown.AddOptions(res);
            Screen.SetResolution(Resolutions.First().width, Resolutions.First().height, Screen.fullScreen);
        }

        public void SetResolution()
        {
            Screen.SetResolution(Resolutions[Dropdown.value].width, Resolutions[Dropdown.value].height, Screen.fullScreen);
        }

        public void ChangeScreenMode()
        {
            Screen.fullScreen = Toggle.isOn;
        }
    }
}
