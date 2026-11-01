using System;
using UnityEngine;
using NightRacer.Persistence;

namespace NightRacer.Player
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private PlayerStats stats;
        [SerializeField] private float pointsPerMeter = 1f;
        [SerializeField] private int pointsPerCoin = 50;

        private float startZ;

        public float Distance { get; private set; }
        public int Score { get; private set; }
        public int HighScore { get; private set; }

        public event Action<int> OnScoreChanged;
        public event Action<int> OnHighScoreChanged;

        private void Awake()
        {
            HighScore = HighScoreStorage.Load();
        }

        private void Start()
        {
            if (player != null)
            {
                startZ = player.position.z;
            }
        }

        private void Update()
        {
            if (player == null)
            {
                return;
            }

            Distance = Mathf.Max(0f, player.position.z - startZ);
            int coins = stats != null ? stats.Coins : 0;
            int currentScore = Mathf.FloorToInt(Distance * pointsPerMeter) + coins * pointsPerCoin;

            if (currentScore != Score)
            {
                Score = currentScore;
                OnScoreChanged?.Invoke(Score);

                if (Score > HighScore)
                {
                    HighScore = Score;
                    OnHighScoreChanged?.Invoke(HighScore);
                }
            }
        }

        public void SaveHighScore()
        {
            HighScoreStorage.Save(HighScore);
        }
    }
}

