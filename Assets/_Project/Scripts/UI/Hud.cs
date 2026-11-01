using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NightRacer.Player;

namespace NightRacer.UI
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private PlayerStats stats;
        [SerializeField] private PlayerFuel fuel;
        [SerializeField] private ScoreCounter scoreCounter;
        [SerializeField] private TMP_Text speedText;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text highScoreText;
        [SerializeField] private Image fuelFill;
        [SerializeField] private Image[] lifeIcons;
        [SerializeField] private float speedScale = 3.6f;

        private int lastSpeed = -1;

        private void OnEnable()
        {
            if (stats != null)
            {
                stats.OnLivesChanged += UpdateLives;
            }

            if (fuel != null)
            {
                fuel.OnFuelChanged += UpdateFuel;
            }

            if (scoreCounter != null)
            {
                scoreCounter.OnScoreChanged += UpdateScore;
                scoreCounter.OnHighScoreChanged += UpdateHighScore;
            }
        }

        private void OnDisable()
        {
            if (stats != null)
            {
                stats.OnLivesChanged -= UpdateLives;
            }

            if (fuel != null)
            {
                fuel.OnFuelChanged -= UpdateFuel;
            }

            if (scoreCounter != null)
            {
                scoreCounter.OnScoreChanged -= UpdateScore;
                scoreCounter.OnHighScoreChanged -= UpdateHighScore;
            }
        }

        private void Start()
        {
            if (stats != null)
            {
                UpdateLives(stats.Lives);
            }

            if (fuel != null)
            {
                UpdateFuel(fuel.Normalized);
            }

            if (scoreCounter != null)
            {
                UpdateScore(scoreCounter.Score);
                UpdateHighScore(scoreCounter.HighScore);
            }
        }

        private void Update()
        {
            if (movement == null || speedText == null)
            {
                return;
            }

            int currentSpeed = Mathf.RoundToInt(movement.Speed * speedScale);
            if (currentSpeed != lastSpeed)
            {
                lastSpeed = currentSpeed;
                speedText.SetText("{0} KM/H", currentSpeed);
            }
        }

        private void UpdateScore(int score)
        {
            if (scoreText != null)
            {
                scoreText.SetText("{0}", score);
            }
        }

        private void UpdateHighScore(int highScore)
        {
            if (highScoreText != null)
            {
                highScoreText.SetText("{0}", highScore);
            }
        }

        private void UpdateFuel(float ratio)
        {
            if (fuelFill != null)
            {
                fuelFill.fillAmount = ratio;
            }
        }

        private void UpdateLives(int lives)
        {
            if (lifeIcons == null)
            {
                return;
            }

            for (int i = 0; i < lifeIcons.Length; i++)
            {
                if (lifeIcons[i] != null)
                {
                    lifeIcons[i].gameObject.SetActive(i < lives);
                }
            }
        }
    }
}

