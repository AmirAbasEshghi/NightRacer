using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NightRacer.Core;
using NightRacer.Player;

namespace NightRacer.UI
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text highScoreText;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button menuButton;
        [SerializeField] private ScoreCounter scoreCounter;

        private void Awake()
        {
            if (restartButton != null)
            {
                restartButton.onClick.AddListener(OnRestartClicked);
            }

            if (menuButton != null)
            {
                menuButton.onClick.AddListener(OnMenuClicked);
            }
        }

        private void Start()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged += HandleStateChanged;
                HandleStateChanged(GameManager.Instance.State);
            }
        }

        private void OnDestroy()
        {
            if (restartButton != null)
            {
                restartButton.onClick.RemoveListener(OnRestartClicked);
            }

            if (menuButton != null)
            {
                menuButton.onClick.RemoveListener(OnMenuClicked);
            }

            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged -= HandleStateChanged;
            }
        }

        private void HandleStateChanged(GameState state)
        {
            bool isGameOver = state == GameState.GameOver;
            if (panel != null)
            {
                panel.SetActive(isGameOver);
            }

            if (isGameOver && scoreCounter != null)
            {
                if (scoreText != null)
                {
                    scoreText.SetText("{0}", scoreCounter.Score);
                }

                if (highScoreText != null)
                {
                    highScoreText.SetText("{0}", scoreCounter.HighScore);
                }
            }
        }

        private void OnRestartClicked()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.Restart();
            }
        }

        private void OnMenuClicked()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoadMenu();
            }
        }
    }
}

