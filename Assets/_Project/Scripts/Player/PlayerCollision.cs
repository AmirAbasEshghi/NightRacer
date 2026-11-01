using UnityEngine;
using NightRacer.Interactables;

namespace NightRacer.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField] private PlayerStats stats;
        [SerializeField] private PlayerFuel fuel;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Interactable item) && !item.IsUsed)
            {
                item.Interact(stats, fuel);
            }
        }
    }
}

