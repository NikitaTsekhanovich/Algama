using System.Collections.Generic;
using Photon.Pun;

namespace GameLogic
{
    public class Levels : MonoBehaviourPunCallbacks
    {
        protected Queue<string> levels = new Queue<string>();
        protected int numberLevels = 5;

        private void Awake()
        {
            for (var i = 0; i < numberLevels; i++)
            {
                levels.Enqueue("Game");
            }
        }
    }
}
