using UnityEngine;
using NightRacer.Player;

namespace NightRacer.Interactables
{
    public abstract class Interactable : MonoBehaviour
    {
        public bool IsUsed { get; protected set; }

        public virtual void Interact(PlayerStats stats, PlayerFuel fuel)
        {
            IsUsed = true;
        }

        protected virtual void OnEnable()
        {
            IsUsed = false;
        }
    }
}

