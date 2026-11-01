using UnityEngine;
using UnityEngine.UI;
using NightRacer.Core;

namespace NightRacer.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button menuButton;

        private void Awake()
        {
            if (resumeButton != null)
            {
                resumeButton.onClick.AddListener(OnResumeClicked);
            }

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
            if (resumeButton != null)
            {
                resumeButton.onClick.RemoveListener(OnResumeClicked);
            }

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
            if (panel != null)
            {
                panel.SetActive(state == GameState.Paused);
            }
        }

        private void OnResumeClicked()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.Resume();
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

