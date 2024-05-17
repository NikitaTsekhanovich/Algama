using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Spells;
using UnityEngine;
using Utils;

namespace Players
{
    public class Casting : MonoBehaviour
    {
        private const int elementsCount = 3;
        private CycleList<GameObject> _elements;

        [SerializeField] private GameObject FireElement;
        [SerializeField] private GameObject WaterElement;
        [SerializeField] private GameObject LightnigElement;
        [SerializeField] private GameObject ShieldElement;
        [SerializeField] private GameObject EarthElement;

        [SerializeField] private Transform ElementsHolder;

        public Casting()
        {
            _elements = new CycleList<GameObject>(elementsCount);
        }
        
        public void CastElement(MagickElementSource source)
        {
            GameObject magick = source switch
            {
                MagickElementSource.Fire => Instantiate(FireElement),
                MagickElementSource.Wind => Instantiate(WaterElement),
                MagickElementSource.Lightning => Instantiate(LightnigElement),
                MagickElementSource.Shield => Instantiate(ShieldElement),
                MagickElementSource.Earth => Instantiate(EarthElement)
            };
            
            magick.transform.SetParent(ElementsHolder);

            var index = _elements.Add(magick);
            magick.transform.position = ElementsHolder.GetChild(index).transform.position;
        }

        public Pattern CreateActivePattern()
        {
            var elementsSources = _elements
                .GetCastedElements()
                .Select(e => e.GetComponent<MagickElement>().source)
                .ToArray();
            
            return new Pattern(elementsSources);
        }

        public void ClearElements() => _elements.Clear();
    }
}