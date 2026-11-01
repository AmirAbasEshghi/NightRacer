using System;
using UnityEngine;

namespace NightRacer.Player
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private int maxLives = 3;

        private int lives;
        private int coins;

        public int Lives => lives;
        public int Coins => coins;

        public event Action<int> OnLivesChanged;
        public event Action<int> OnCoinsChanged;
        public event Action OnHit;
        public event Action OnDied;

        private void Awake()
        {
            lives = maxLives;
        }

        public void TakeHit()
        {
            if (lives <= 0)
            {
                return;
            }

            lives--;
            OnHit?.Invoke();
            OnLivesChanged?.Invoke(lives);

            if (lives == 0)
            {
                OnDied?.Invoke();
            }
        }

        public void AddCoin(int value)
        {
            coins += value;
            OnCoinsChanged?.Invoke(coins);
        }
    }
}

