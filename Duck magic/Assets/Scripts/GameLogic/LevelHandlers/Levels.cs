using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace GameLogic.LevelHandlers
{
    public class Levels : MonoBehaviour
    {
        [SerializeField] private List<string> _levels = new List<string>();
        protected Queue<string> currentLevels = new Queue<string>();
        private const int MaxLevels = 3;

        protected void CreateLevelsQueue()
        {
            var randomIndexes = ChooseRandomIndexes();
            ChooseRandomLevels(randomIndexes);
        }

        private void ChooseRandomLevels(int[] randomIndexes)
        {
            for (var i = 0; i < MaxLevels; i++)
            {
                currentLevels.Enqueue(_levels[randomIndexes[i]]);
            }
        }

        private int[] ChooseRandomIndexes()
        {
            var random = new Random();
            
            return Enumerable.Range(0, _levels.Count)
                .OrderBy(_ => random.Next())
                .ToArray();
        }
    }
}
