using Photon.Pun;
using Players;
using GameItems.DamageDealers.Dynamites;

namespace Spells.Properties
{
    public interface ICanDealDamage : ICanImpact
    {
        public float DamageOnPlayer { get; set; }
        public float? DamageOnProp { get; set; }

        public void DealDamageTo<TPlayerHealth>(TPlayerHealth healthHandler, PhotonView view)
            where TPlayerHealth : HealthHandler;

        public void DealDestroyTo<TDestroyableObject>(TDestroyableObject healthHandler, PhotonView view)
            where TDestroyableObject : HealthHandlerItems;
    }
}