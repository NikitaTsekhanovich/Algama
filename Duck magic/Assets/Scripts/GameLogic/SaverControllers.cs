using UnityEngine;

namespace GameLogic
{
    public class SaverControllers : MonoBehaviour
    {
        private void Awake()
        {
            var gameManager = GameObject.FindGameObjectsWithTag("Controllers");

            if (gameManager.Length > 1)
            {
                Destroy(gameObject);
            }
            
            DontDestroyOnLoad(gameObject);
        }
    }
}
