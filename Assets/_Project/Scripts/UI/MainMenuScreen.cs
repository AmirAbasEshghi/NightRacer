using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using NightRacer.Core;
using NightRacer.Persistence;

namespace NightRacer.UI
{
    public class MainMenuScreen : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private TMP_Text highScoreText;
        [SerializeField] private string gameSceneName = "Game";

        private void Awake()
        {
            if (startButton != null)
            {
                startButton.onClick.AddListener(OnStartClicked);
            }

            if (quitButton != null)
            {
                quitButton.onClick.AddListener(OnQuitClicked);
            }
        }

        private void Start()
        {
            if (highScoreText != null)
            {
                highScoreText.SetText("{0}", HighScoreStorage.Load());
            }
        }

        private void OnDestroy()
        {
            if (startButton != null)
            {
                startButton.onClick.RemoveListener(OnStartClicked);
            }

            if (quitButton != null)
            {
                quitButton.onClick.RemoveListener(OnQuitClicked);
            }
        }

        private void OnStartClicked()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.StartGame();
            }
            else
            {
                SceneManager.LoadScene(gameSceneName);
            }
        }

        private void OnQuitClicked()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.Quit();
            }
            else
            {
                Application.Quit();
            }
        }
    }
}

