using UnityEngine;
using NightRacer.Core;
using NightRacer.Interactables;
using NightRacer.Player;

namespace NightRacer.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource crashSource;
        [SerializeField] private AudioClip musicClip;
        [SerializeField] private AudioClip coinClip;
        [SerializeField] private AudioClip fuelClip;
        [SerializeField] private PlayerStats stats;
        [SerializeField] private float crashStartTime = 1f;
        [SerializeField] private float normalMusicVolume = 0.8f;
        [SerializeField] private float pausedMusicVolume = 0.25f;
        [SerializeField] private float gameOverMusicVolume = 0.2f;

        private void OnEnable()
        {
            if (stats != null)
            {
                stats.OnHit += HandleHit;
            }

            Interactable.OnPickupCollected += HandlePickup;
        }

        private void OnDisable()
        {
            if (stats != null)
            {
                stats.OnHit -= HandleHit;
            }

            Interactable.OnPickupCollected -= HandlePickup;
        }

        private void Start()
        {
            if (musicSource != null && musicClip != null)
            {
                musicSource.clip = musicClip;
                musicSource.loop = true;
                if (!musicSource.isPlaying)
                {
                    musicSource.Play();
                }
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
            if (musicSource == null)
            {
                return;
            }

            if (state == GameState.Playing || state == GameState.Resuming)
            {
                musicSource.volume = normalMusicVolume;
                if (!musicSource.isPlaying)
                {
                    musicSource.Play();
                }
            }
            else if (state == GameState.Paused)
            {
                musicSource.volume = pausedMusicVolume;
            }
            else if (state == GameState.GameOver)
            {
                musicSource.volume = gameOverMusicVolume;
            }
        }

        private void HandleHit()
        {
            crashSource.time = crashStartTime;
            crashSource.Play();
        }

        private void HandlePickup(Interactable item, Vector3 position)
        {
            if (sfxSource == null)
            {
                return;
            }

            if (item is Coin && coinClip != null)
            {
                sfxSource.PlayOneShot(coinClip);
            }
            else if (item is FuelCan && fuelClip != null)
            {
                sfxSource.PlayOneShot(fuelClip);
            }
        }
    }
}
