using Photon.Pun;
using Players;

namespace Spells.Properties
{
    public interface ICanDealDamage : ICanImpact
    {
        public float DamageOnPlayer { get; set; }
        public float? DamageOnProp { get; set; }

        public void DealDamageTo<TPlayerHealth>(TPlayerHealth healthHandler, PhotonView view)
            where TPlayerHealth : HealthHandler;
    }
}