using UnityEngine;
using NightRacer.Core;
using NightRacer.Road;
using NightRacer.PlayerInput;

namespace NightRacer.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private RoadSettings roadSettings;
        [SerializeField] private InputReader inputReader;
        [SerializeField] private float startSpeed = 20f;
        [SerializeField] private float laneChangeSpeed = 12f;

        public float Speed { get; set; }

        private int targetLane;

        private void Awake()
        {
            Speed = startSpeed;
        }

        private void Start()
        {
            targetLane = roadSettings.CenterLane;
            Vector3 pos = transform.position;
            pos.x = roadSettings.GetLaneX(targetLane);
            transform.position = pos;
        }

        private void OnEnable()
        {
            if (inputReader != null)
            {
                inputReader.OnLaneChange += HandleLaneChange;
            }
        }

        private void OnDisable()
        {
            if (inputReader != null)
            {
                inputReader.OnLaneChange -= HandleLaneChange;
            }
        }

        private void Update()
        {
            Vector3 pos = transform.position;
            pos.z += Speed * Time.deltaTime;

            float targetX = roadSettings.GetLaneX(targetLane);
            pos.x = Mathf.MoveTowards(pos.x, targetX, laneChangeSpeed * Time.deltaTime);

            transform.position = pos;
        }

        private void HandleLaneChange(int direction)
        {
            if (GameManager.Instance != null && GameManager.Instance.State != GameState.Playing)
            {
                return;
            }

            targetLane = Mathf.Clamp(targetLane + direction, 0, roadSettings.LaneCount - 1);
        }
    }
}

