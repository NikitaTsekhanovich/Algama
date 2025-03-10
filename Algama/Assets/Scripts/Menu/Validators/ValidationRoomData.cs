using TMPro;
using UnityEngine;

namespace Menu.Validators
{
    public class ValidationRoomData : MonoBehaviour
    {
        [SerializeField] private GameObject _errorInputRoomWindow;
        [SerializeField] private TMP_Text _errorText;
        
        public bool IsCorrectRoomName(string roomName)
        {
            if (roomName.Length != 0 && roomName.Length <= 16)
                return true;
            
            _errorInputRoomWindow.SetActive(true);
            return false;
        }
    }
}
