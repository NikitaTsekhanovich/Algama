using Interfaces;
using Menu.MenuHandlers;
using Photon.Pun;

namespace GameLogic
{
    public class LevelSystem : Levels, IObserver
    {
        public void OnEnable()
        {
            MenuHandler.OnLoadLevel += StartLevel;
            GameSystem.OnEndLevel += EndLevel;
        }

        public void OnDisable()
        {
            MenuHandler.OnLoadLevel -= StartLevel;
            GameSystem.OnEndLevel -= EndLevel;
        }
        
        private void StartLevel()
        {
            var currentLevel = levels.Dequeue();
            PhotonNetwork.LoadLevel(currentLevel);
        }

        private void EndLevel()
        {
            // показываем очки через коррутину, ждем 5 сек и ->
            if (levels.Count >= 1)
            {
                StartLevel();
            }
            else
            {
                // желательно выйти в лобби 
                // или выкинуть всех в меню 
            }
        }
    }
}
