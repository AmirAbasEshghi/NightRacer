using UnityEngine;
using NightRacer.Player;
using NightRacer.Interactables;

namespace NightRacer.Difficulty
{
    public class DifficultyScaler : MonoBehaviour
    {
        [SerializeField] private ScoreCounter scoreCounter;
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private InteractableSpawner spawner;

        [SerializeField] private float distanceToMax = 3000f;
        [SerializeField] private float maxSpeed = 60f;
        [SerializeField] private float endRowSpacing = 35f;
        [SerializeField] private float endObstacleChance = 0.7f;
        [SerializeField] private float hitSlowdown = 5f;

        private PlayerStats stats;
        private float startSpeed;
        private float startRowSpacing;
        private float startObstacleChance;
        private float speedPenalty;

        private void Awake()
        {
            if (movement != null)
            {
                stats = movement.GetComponent<PlayerStats>();
            }
        }

        private void OnEnable()
        {
            if (stats == null && movement != null)
            {
                stats = movement.GetComponent<PlayerStats>();
            }

            if (stats != null)
            {
                stats.OnHit += HandleHit;
            }
        }

        private void OnDisable()
        {
            if (stats != null)
            {
                stats.OnHit -= HandleHit;
            }
        }

        private void Start()
        {
            if (movement != null)
            {
                startSpeed = movement.Speed;
            }

            if (spawner != null)
            {
                startRowSpacing = spawner.RowSpacing;
                startObstacleChance = spawner.ObstacleChance;
            }
        }

        private void Update()
        {
            if (scoreCounter == null || distanceToMax <= 0f)
            {
                return;
            }

            float t = Mathf.Clamp01(scoreCounter.Distance / distanceToMax);

            if (movement != null)
            {
                float lerpedSpeed = Mathf.Lerp(startSpeed, maxSpeed, t);
                movement.Speed = Mathf.Max(startSpeed, lerpedSpeed - speedPenalty);
            }

            if (spawner != null)
            {
                spawner.RowSpacing = Mathf.Lerp(startRowSpacing, endRowSpacing, t);
                spawner.ObstacleChance = Mathf.Lerp(startObstacleChance, endObstacleChance, t);
            }
        }

        private void HandleHit()
        {
            if (movement == null)
            {
                return;
            }

            float excess = Mathf.Max(0f, movement.Speed - startSpeed);
            float reduction = Mathf.Min(hitSlowdown, excess);
            speedPenalty += reduction;
            movement.Speed = Mathf.Max(startSpeed, movement.Speed - reduction);
        }
    }
}
