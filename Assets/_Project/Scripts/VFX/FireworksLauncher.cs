using UnityEngine;
using NightRacer.Core;

namespace NightRacer.VFX
{
    public class FireworksLauncher : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private ParticleSystem[] fireworks;
        [SerializeField] private float minInterval = 1.5f;
        [SerializeField] private float maxInterval = 4f;
        [SerializeField] private float forwardDistanceMin = 120f;
        [SerializeField] private float forwardDistanceMax = 180f;
        [SerializeField] private float heightMin = 40f;
        [SerializeField] private float heightMax = 70f;
        [SerializeField] private float spreadX = 60f;
        [SerializeField] private Color[] colors;

        private float timer;
        private int currentIndex;

        private void Start()
        {
            timer = Random.Range(minInterval, maxInterval);
        }

        private void Update()
        {
            if (GameManager.Instance == null || GameManager.Instance.State != GameState.Playing)
            {
                return;
            }

            if (player == null || fireworks == null || fireworks.Length == 0)
            {
                return;
            }

            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                Launch();
                timer = Random.Range(minInterval, maxInterval);
            }
        }

        private void Launch()
        {
            float z = player.position.z + Random.Range(forwardDistanceMin, forwardDistanceMax);
            float y = Random.Range(heightMin, heightMax);
            float x = Random.Range(-spreadX, spreadX);

            ParticleSystem ps = fireworks[currentIndex];
            currentIndex = (currentIndex + 1) % fireworks.Length;

            if (colors != null && colors.Length > 0)
            {
                Color color = colors[Random.Range(0, colors.Length)];
                var main = ps.main;
                main.startColor = color;
            }

            ps.transform.position = new Vector3(x, y, z);
            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            ps.Play();
        }
    }
}

