using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using NightRacer.PlayerInput;

namespace NightRacer.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private GameState initialState = GameState.Playing;
        [SerializeField] private string menuSceneName = "MainMenu";
        [SerializeField] private string gameSceneName = "Game";
        [SerializeField] private InputReader inputReader;

        public GameState State { get; private set; }
        public event Action<GameState> OnStateChanged;

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            if (inputReader != null)
            {
                inputReader.OnPause += TogglePause;
            }
        }

        private void Start()
        {
            SetState(initialState);
        }

        private void OnDisable()
        {
            if (inputReader != null)
            {
                inputReader.OnPause -= TogglePause;
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }

            Time.timeScale = 1f;
        }

        public void SetState(GameState newState)
        {
            State = newState;
            UpdateTimeScale();
            OnStateChanged?.Invoke(State);
        }

        public void Pause()
        {
            if (State == GameState.Playing)
            {
                SetState(GameState.Paused);
            }
        }

        public void Resume()
        {
            if (State == GameState.Paused)
            {
                SetState(GameState.Playing);
            }
        }

        public void TogglePause()
        {
            if (State == GameState.Playing)
            {
                Pause();
            }
            else if (State == GameState.Paused)
            {
                Resume();
            }
        }

        public void GameOver()
        {
            SetState(GameState.GameOver);
        }

        public void StartGame()
        {
            SceneManager.LoadScene(gameSceneName);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LoadMenu()
        {
            SceneManager.LoadScene(menuSceneName);
        }

        public void Quit()
        {
            Application.Quit();
        }

        private void UpdateTimeScale()
        {
            if (State == GameState.Playing || State == GameState.Menu)
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f;
            }
        }
    }
}

