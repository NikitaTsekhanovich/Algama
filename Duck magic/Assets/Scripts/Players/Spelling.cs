using System.Collections.Generic;
using Photon.Pun;
using Spells;
using UnityEngine;

namespace Players
{
    public class Spelling : MonoBehaviour
    {
        private Casting _casting;
        
        [SerializeField] private GameObject missile;
        [SerializeField] private GameObject tornado;
        [SerializeField] private GameObject earthbound;
        [SerializeField] private GameObject magickShield;
        [SerializeField] private GameObject stoneShield;
        
        [SerializeField] private Transform throwPoint;

        private Dictionary<(Pattern, int), GameObject> _patternToSpell;

        private void Start()
        {
            _casting = GetComponent<Casting>();

            _patternToSpell = new()
            {
                [(new Pattern(MagickElementSource.Wind, MagickElementSource.Wind), 1)] = tornado,
                [(new Pattern(MagickElementSource.Earth, MagickElementSource.Earth), 1)] = earthbound,
                [(new Pattern(MagickElementSource.Fire, MagickElementSource.Fire), 1)] = missile,
                [(new Pattern(MagickElementSource.Shield), 2)] = magickShield,
                [(new Pattern(MagickElementSource.Shield, MagickElementSource.Earth), 3)] = stoneShield
            };
        }

        public void CastSpell()
        {
            var currentPattern = _casting.CreateActivePattern();
            if (currentPattern.Length == 0)
                return;

            GameObject candidate = null;
            var currentMaxPriority = 0;
            
            foreach (var ((pattern, priority), spellObject) in _patternToSpell)
            {
                if (currentPattern.Equals(pattern) && priority > currentMaxPriority)
                {
                    candidate = spellObject;
                    currentMaxPriority = priority;
                }
            }

            if (candidate is not null)
            {
                Instantiate(candidate, throwPoint.position, throwPoint.rotation);
                Debug.Log($"Casting {candidate.name}");
            }
            else
            {
                Debug.Log("Oops, no combination for that :(");
            }
            
            _casting.ClearElements();
        }
    }
}