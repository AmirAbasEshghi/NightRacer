using UnityEngine;
using NightRacer.Player;

namespace NightRacer.VFX
{
    public class HitEffects : MonoBehaviour
    {
        [SerializeField] private PlayerStats stats;
        [SerializeField] private ParticlePool sparkPool;
        [SerializeField] private CameraShake cameraShake;
        [SerializeField] private DamageFlash damageFlash;

        private void OnEnable()
        {
            if (stats != null)
            {
                stats.OnHit += HandleHit;
            }
        }

        private void OnDisable()
        {
            if (stats != null)
            {
                stats.OnHit -= HandleHit;
            }
        }

        private void HandleHit()
        {
            if (sparkPool != null)
            {
                sparkPool.Play(transform.position);
            }

            if (cameraShake != null)
            {
                cameraShake.Shake();
            }

            if (damageFlash != null)
            {
                damageFlash.Flash();
            }
        }
    }
}

