using UnityEngine;

namespace NightRacer.VFX
{
    public class ParticlePool : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] particles;

        private int currentIndex;

        public void Play(Vector3 position)
        {
            if (particles == null || particles.Length == 0)
            {
                return;
            }

            for (int i = 0; i < particles.Length; i++)
            {
                if (!particles[i].isPlaying)
                {
                    particles[i].transform.position = position;
                    particles[i].Play();
                    return;
                }
            }

            ParticleSystem ps = particles[currentIndex];
            currentIndex = (currentIndex + 1) % particles.Length;
            ps.transform.position = position;
            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            ps.Play();
        }
    }
}

