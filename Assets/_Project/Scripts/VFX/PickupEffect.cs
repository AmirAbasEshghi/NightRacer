using UnityEngine;
using NightRacer.Interactables;

namespace NightRacer.VFX
{
    public class PickupEffect : MonoBehaviour
    {
        [SerializeField] private ParticlePool pool;

        private void OnEnable()
        {
            Interactable.OnPickupCollected += HandlePickup;
        }

        private void OnDisable()
        {
            Interactable.OnPickupCollected -= HandlePickup;
        }

        private void HandlePickup(Interactable item, Vector3 position)
        {
            if (pool != null)
            {
                pool.Play(position);
            }
        }
    }
}
