using UnityEngine;
using NightRacer.Core;
using NightRacer.Player;

namespace NightRacer.Audio
{
    public class EngineSound : MonoBehaviour
    {
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private AudioSource source;
        [SerializeField] private float minSpeed = 20f;
        [SerializeField] private float maxSpeed = 60f;
        [SerializeField] private float minPitch = 0.8f;
        [SerializeField] private float maxPitch = 2f;
        [SerializeField] private float minVolume = 0.3f;
        [SerializeField] private float maxVolume = 0.8f;
        [SerializeField] private float loopTrim = 1f;

        private void Start()
        {
            if (source != null)
            {
                source.loop = true;
            }

            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged += HandleStateChanged;
                HandleStateChanged(GameManager.Instance.State);
            }
        }

        private void OnDestroy()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged -= HandleStateChanged;
            }
        }

        private void HandleStateChanged(GameState state)
        {
            if (source == null)
            {
                return;
            }

            if (state == GameState.Playing)
            {
                if (!source.isPlaying)
                {
                    source.Play();
                }
                else
                {
                    source.UnPause();
                }
            }
            else
            {
                if (source.isPlaying)
                {
                    source.Pause();
                }
            }
        }

        private void Update()
        {
            if (movement == null || source == null)
            {
                return;
            }

            if (GameManager.Instance != null && GameManager.Instance.State != GameState.Playing)
            {
                return;
            }

            if (source.isPlaying && source.time >= source.clip.length - loopTrim)
            {
                source.time = 0f;
            }

            float t = Mathf.InverseLerp(minSpeed, maxSpeed, movement.Speed);
            source.pitch = Mathf.Lerp(minPitch, maxPitch, t);
            source.volume = Mathf.Lerp(minVolume, maxVolume, t);
        }
    }
}
