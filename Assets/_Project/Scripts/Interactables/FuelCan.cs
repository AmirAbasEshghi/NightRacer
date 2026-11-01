using UnityEngine;
using NightRacer.Player;

namespace NightRacer.Interactables
{
    public class FuelCan : Interactable
    {
        [SerializeField] private float amount = 35f;

        public override void Interact(PlayerStats stats, PlayerFuel fuel)
        {
            base.Interact(stats, fuel);
            if (fuel != null)
            {
                fuel.Refill(amount);
            }
            NotifyPickupCollected();
        }
    }
}
