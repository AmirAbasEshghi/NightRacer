using UnityEngine;
using NightRacer.Player;

namespace NightRacer.Interactables
{
    public class Coin : Interactable
    {
        [SerializeField] private int value = 1;

        public override void Interact(PlayerStats stats, PlayerFuel fuel)
        {
            base.Interact(stats, fuel);
            if (stats != null)
            {
                stats.AddCoin(value);
            }
            NotifyPickupCollected();
        }
    }
}
