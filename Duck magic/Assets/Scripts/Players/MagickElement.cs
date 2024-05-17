using UnityEngine;

namespace Players
{
    public enum MagickElementSource
    {
        Fire,
        Wind,
        Lightning,
        Earth,
        Shield
    }

    public enum MagickElementDensity
    {
        Solid,
        Intangible
    }

    public class MagickElement : MonoBehaviour
    {
        [SerializeField] public MagickElementSource source;
        [SerializeField] public MagickElementDensity density;
    }
}