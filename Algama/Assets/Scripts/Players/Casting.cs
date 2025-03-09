using System.Linq;
using Spells;
using UnityEngine;
using Utils;
using Photon.Pun;

namespace Players
{
    public class Casting : MonoBehaviour
    {
        private const int elementsCount = 3;
        private CycleList<GameObject> _elements;
        protected PhotonView _photonView;

        [SerializeField] private GameObject FireElement;
        [SerializeField] private GameObject WaterElement;
        [SerializeField] private GameObject LightnigElement;
        [SerializeField] private GameObject ShieldElement;
        [SerializeField] private GameObject EarthElement;

        [SerializeField] private Transform ElementsHolder;

        private void Start()
        {
            _photonView = GetComponent<PhotonView>();
        }

        public Casting()
        {
            _elements = new CycleList<GameObject>(elementsCount);
        }

        public void CastElement(MagickElementSource source)
        {
            _photonView.RPC("SynchronizeCast", RpcTarget.All, source);
        }

        [PunRPC]
        private void SynchronizeCast(MagickElementSource source)
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

        public (Pattern currentPattern, float manaCost) CreateActivePattern()
        {
            var elements = _elements
                .GetCastedElements()
                .Select(e => e.GetComponent<MagickElement>())
                .ToArray();

            var manaCost = elements.Sum(e => e.manaCost) / 100f;

            return (new Pattern(elements.Select(e => e.source).ToArray()), manaCost);
        }

        public void ClearElements() => _elements.Clear();
    }
}