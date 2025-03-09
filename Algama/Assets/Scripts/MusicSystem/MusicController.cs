using System.Collections;
using System.Collections.Generic;
using GameLogic.LevelHandlers;
using Interfaces;
using PlayerMenu;
using UnityEngine;

namespace MusicSystem
{
    public class MusicController : MonoBehaviour
    {
        [SerializeField] private AudioSource _menuAudio;
        [SerializeField] private AudioSource _gameAudio;
        [SerializeField] private List<AudioClip> _gameMusics = new();
        private Queue<AudioClip> _currentMusics = new();

        private void Start()
        {
            CreateMusicQueue();
        }

        private void CreateMusicQueue()
        {
            foreach (var music in _gameMusics)
                _currentMusics.Enqueue(music);
        }
        
        public void OnEnable()
        {
            LevelLoader.OnMenuMusic += StartMenuMusic;
            LevelLoader.OffMenuMusic += StopMenuMusic;
            LevelLoader.OnGameMusic += StartGameMusic;
            LevelLoader.OffGameMusic += StopGameMusic;
            PlayerMenuHandler.OnMenuMusic += StartMenuMusic;
            PlayerMenuHandler.OffGameMusic += StopGameMusic;
        }

        public void OnDisable()
        {
            LevelLoader.OnMenuMusic -= StartMenuMusic;
            LevelLoader.OffMenuMusic -= StopMenuMusic;
            LevelLoader.OnGameMusic -= StartGameMusic;
            LevelLoader.OffGameMusic -= StopGameMusic;
            PlayerMenuHandler.OnMenuMusic -= StartMenuMusic;
            PlayerMenuHandler.OffGameMusic -= StopGameMusic;
        }

        private void StartMenuMusic()
        {
            _menuAudio.Play();
        }

        private void StopMenuMusic()
        {
            _menuAudio.Stop();
        }

        private void StartGameMusic()
        {
            if (_currentMusics.Count == 0)
            {
                CreateMusicQueue();
            }
            _gameAudio.clip = _currentMusics.Dequeue();
            _gameAudio.Play();
        }

        private void StopGameMusic()
        {
            _gameAudio.Stop();
        }
    }
}

