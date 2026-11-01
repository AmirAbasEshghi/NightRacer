using UnityEngine;
using NightRacer.Player;

namespace NightRacer.Core
{
    public class GameOverCoordinator : MonoBehaviour
    {
        [SerializeField] private PlayerStats stats;
        [SerializeField] private PlayerFuel fuel;
        [SerializeField] private ScoreCounter scoreCounter;

        private void OnEnable()
        {
            if (stats != null)
            {
                stats.OnDied += HandleGameOver;
            }

            if (fuel != null)
            {
                fuel.OnFuelEmpty += HandleGameOver;
            }
        }

        private void OnDisable()
        {
            if (stats != null)
            {
                stats.OnDied -= HandleGameOver;
            }

            if (fuel != null)
            {
                fuel.OnFuelEmpty -= HandleGameOver;
            }
        }

        private void HandleGameOver()
        {
            if (GameManager.Instance == null || GameManager.Instance.State != GameState.Playing)
            {
                return;
            }

            if (scoreCounter != null)
            {
                scoreCounter.SaveHighScore();
            }

            GameManager.Instance.GameOver();
        }
    }
}

