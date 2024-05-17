using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public class CycleList<TItem> : MonoBehaviour where TItem : Object
    {
        private int _currentIndex = 0;
        private readonly TItem[] _internalArray;

        public CycleList(int cycleCapacity)
        {
            _internalArray = new TItem[cycleCapacity];
            Debug.Log(cycleCapacity);
        }

        public int Add(TItem item)
        {
            if (_currentIndex > _internalArray.Length - 1)
                _currentIndex = 0;
            
            Destroy(_internalArray[_currentIndex]);

            _internalArray[_currentIndex] = item;
            var currentResult = _currentIndex;
            
            _currentIndex++;

            return currentResult;
        }

        public IReadOnlyCollection<TItem> GetCastedElements()
        {
            return _internalArray.TakeWhile(t => t is not null).ToList();
        }

        public void Clear()
        {
            for (var i = 0; i < _internalArray.Length; i++)
            {
                Destroy(_internalArray[i]);
                _internalArray[i] = null;
            }
                

            _currentIndex = 0;
        }
    }
}