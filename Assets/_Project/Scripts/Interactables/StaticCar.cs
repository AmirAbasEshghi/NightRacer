using NightRacer.Player;

namespace NightRacer.Interactables
{
    public class StaticCar : Interactable
    {
        public override void Interact(PlayerStats stats, PlayerFuel fuel)
        {
            base.Interact(stats, fuel);
            if (stats != null)
            {
                stats.TakeHit();
            }
        }
    }
}

