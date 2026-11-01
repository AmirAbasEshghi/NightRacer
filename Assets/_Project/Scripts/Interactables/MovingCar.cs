using UnityEngine;
using NightRacer.Player;

namespace NightRacer.Interactables
{
    public class MovingCar : Interactable
    {
        [SerializeField] private float speed = 6f;

        public override void Interact(PlayerStats stats, PlayerFuel fuel)
        {
            base.Interact(stats, fuel);
            if (stats != null)
            {
                stats.TakeHit();
            }
        }

        private void Update()
        {
            transform.position += Vector3.forward * (speed * Time.deltaTime);
        }
    }
}

