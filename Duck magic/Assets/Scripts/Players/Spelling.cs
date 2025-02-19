using System.Collections.Generic;
using Photon.Pun;
using Spells;
using Spells.Types;
using UnityEngine;

namespace Players
{
    public class Spelling : MonoBehaviour
    {
        private Casting _casting;
        private SpriteRenderer _shotDirection;
        private PhotonView _photonView;

        [SerializeField] private GameObject missile;
        [SerializeField] private GameObject tornado;
        [SerializeField] private GameObject earthbound;
        [SerializeField] private GameObject magickShield;
        [SerializeField] private GameObject stoneShield;

        [SerializeField] private Transform rightThrowPoint;
        [SerializeField] private Transform leftThrowPoint;

        private Dictionary<(Pattern, int), GameObject> _patternToSpell;

        private GameObject _candidate;

        private void Start()
        {
            _casting = GetComponent<Casting>();
            _shotDirection = GetComponent<SpriteRenderer>();
            _photonView = GetComponent<PhotonView>();

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
            var result = _casting.CreateActivePattern();
            if (result.currentPattern.Length == 0)
                return;

            _candidate = null;
            var currentMaxPriority = 0;

            foreach (var ((pattern, priority), spellObject) in _patternToSpell)
            {
                if (result.currentPattern.Equals(pattern) && priority > currentMaxPriority)
                {
                    _candidate = spellObject;
                    currentMaxPriority = priority;
                }
            }

            if (_candidate is not null)
            {
                var healthHandler = GetComponent<HealthHandler>();
                var mana = healthHandler.Mana;
                if (mana < result.manaCost)
                    return;

                healthHandler.OnCast(result.manaCost, GetComponent<PhotonView>());

                if (_candidate.name == "EarthShield")
                {
                    var earthShield = PhotonNetwork.Instantiate(_candidate.name, transform.position, Quaternion.identity);
                    earthShield.transform.SetParent(transform);
                }
                else if (!_shotDirection.flipX)
                {
                    PhotonNetwork.Instantiate(_candidate.name, rightThrowPoint.position, rightThrowPoint.rotation);
                }
                else
                {
                    var newSpell = PhotonNetwork.Instantiate(_candidate.name, leftThrowPoint.position, leftThrowPoint.rotation);
                    newSpell.transform.localScale = new Vector3(-newSpell.transform.localScale.x, newSpell.transform.localScale.y, newSpell.transform.localScale.z);
                }

                Debug.Log($"Casting {_candidate.name}");
            }
            else
            {
                Debug.Log("Oops, no combination for that :(");
            }

            _photonView.RPC("SyncClearElements", RpcTarget.All);
        }

        [PunRPC]
        private void SyncClearElements()
        {
            _casting.ClearElements();
        }
    }
}