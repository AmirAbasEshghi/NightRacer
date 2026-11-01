using System;
using UnityEngine;

namespace NightRacer.Player
{
    public class PlayerFuel : MonoBehaviour
    {
        [SerializeField] private float maxFuel = 100f;
        [SerializeField] private float drainPerSecond = 2f;

        private float currentFuel;
        private bool isEmpty;

        public float Normalized => maxFuel > 0f ? currentFuel / maxFuel : 0f;

        public event Action<float> OnFuelChanged;
        public event Action OnFuelEmpty;

        private void Awake()
        {
            currentFuel = maxFuel;
        }

        private void Update()
        {
            if (currentFuel <= 0f)
            {
                return;
            }

            currentFuel -= drainPerSecond * Time.deltaTime;
            if (currentFuel <= 0f)
            {
                currentFuel = 0f;
                if (!isEmpty)
                {
                    isEmpty = true;
                    OnFuelEmpty?.Invoke();
                }
            }

            OnFuelChanged?.Invoke(Normalized);
        }

        public void Refill(float amount)
        {
            currentFuel = Mathf.Clamp(currentFuel + amount, 0f, maxFuel);
            if (currentFuel > 0f)
            {
                isEmpty = false;
            }

            OnFuelChanged?.Invoke(Normalized);
        }
    }
}

