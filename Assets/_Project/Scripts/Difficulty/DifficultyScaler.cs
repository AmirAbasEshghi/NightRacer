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
        [SerializeField] private float maxSpeed = 40f;
        [SerializeField] private float endRowSpacing = 35f;
        [SerializeField] private float endObstacleChance = 0.7f;

        private float startSpeed;
        private float startRowSpacing;
        private float startObstacleChance;

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
                movement.Speed = Mathf.Lerp(startSpeed, maxSpeed, t);
            }

            if (spawner != null)
            {
                spawner.RowSpacing = Mathf.Lerp(startRowSpacing, endRowSpacing, t);
                spawner.ObstacleChance = Mathf.Lerp(startObstacleChance, endObstacleChance, t);
            }
        }
    }
}

