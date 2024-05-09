using System.Collections.Generic;
using System.Linq;
using GameLogic.Items;
using GameLogic.LevelHandlers;
using UnityEngine;

namespace GameLogic.PlayerDataControllers
{
    public class PlayersScoreController : MonoBehaviour
    {
        [SerializeField] private GameObject _scoreWindow;
        [SerializeField] private Transform _scoreContent;
        [SerializeField] private PlayerScoreItem _scoreItem;
        private Dictionary<string, int> _scores = new Dictionary<string, int>();

        public void OnEnable()
        {
            LevelLoader.OnPlayersScore += ShowPlayersScore;
            LevelLoader.OffPlayersScore += ClosePlayersScore;
            LevelLoader.OnClearPlayersScore += ClearScore;
            PlayerDeathController.OnRecordPlayerScore += RecordScore;
        }

        public void OnDisable()
        {
            LevelLoader.OnPlayersScore -= ShowPlayersScore;
            LevelLoader.OffPlayersScore -= ClosePlayersScore;
            LevelLoader.OnClearPlayersScore -= ClearScore;
            PlayerDeathController.OnRecordPlayerScore -= RecordScore;
        }

        private void ClearScore()
        {
            _scores.Clear();
        }

        private void RecordScore(string playerName, int score)
        {
            if (_scores.ContainsKey(playerName))
            {
                _scores[playerName] += score;
            }
            else
            {
                _scores[playerName] = score;
            }
        }

        private void ShowPlayersScore()
        {
            _scoreWindow.SetActive(true);
            ClearScoreBoard();
            FillScoreBoard();
        }
        
        private void ClosePlayersScore()
        {
            _scoreWindow.SetActive(false);
        }

        private void ClearScoreBoard()
        {
            for (var i = 0; i < _scoreContent.childCount; i++)
            {
                Destroy(_scoreContent.GetChild(i).gameObject);
            }
        }

        private void FillScoreBoard()
        {
            var sortedDictionary = _scores
                .OrderBy(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
            
            foreach (var score in sortedDictionary)
            {
                Instantiate(_scoreItem, _scoreContent).SetInfo(score.Key, score.Value);
            }
        }
    }
}
