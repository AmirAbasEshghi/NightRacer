using System.Collections.Generic;
using UnityEngine;
using NightRacer.Pooling;
using NightRacer.Road;

namespace NightRacer.Interactables
{
    public class InteractableSpawner : MonoBehaviour
    {
        [SerializeField] private RoadSettings roadSettings;
        [SerializeField] private Transform player;
        [SerializeField] private Interactable staticCarPrefab;
        [SerializeField] private Interactable movingCarPrefab;
        [SerializeField] private Interactable coinPrefab;
        [SerializeField] private Interactable fuelCanPrefab;

        [SerializeField] private int staticCarCount = 10;
        [SerializeField] private int movingCarCount = 8;
        [SerializeField] private int coinCount = 18;
        [SerializeField] private int fuelCanCount = 4;

        [SerializeField] private float spawnDistance = 100f;
        [SerializeField] private float firstRowDistance = 60f;
        [SerializeField] private float releaseDistance = 15f;
        [SerializeField] private float startRowSpacing = 45f;
        [SerializeField] private float minRowSpacing = 35f;
        [SerializeField] private float startObstacleChance = 0.4f;
        [SerializeField] private float movingCarChance = 0.35f;
        [SerializeField] private float coinChance = 0.5f;
        [SerializeField] private int coinsPerRow = 3;
        [SerializeField] private float coinSpacing = 4f;
        [SerializeField] private float fuelDistanceMin = 200f;
        [SerializeField] private float fuelDistanceMax = 320f;

        private Pool<Interactable> staticCarPool;
        private Pool<Interactable> movingCarPool;
        private Pool<Interactable> coinPool;
        private Pool<Interactable> fuelCanPool;

        private List<Interactable> activeItems;

        private bool[] previousMoving;
        private bool[] currentMoving;

        private float rowSpacing;
        private float obstacleChance;
        private float nextRowZ;
        private float nextFuelZ;
        private int lastSafeLane;

        public float RowSpacing
        {
            get => rowSpacing;
            set => rowSpacing = Mathf.Max(value, minRowSpacing);
        }

        public float ObstacleChance
        {
            get => obstacleChance;
            set => obstacleChance = value;
        }

        private void Awake()
        {
            staticCarPool = new Pool<Interactable>(staticCarPrefab, transform, staticCarCount);
            movingCarPool = new Pool<Interactable>(movingCarPrefab, transform, movingCarCount);
            coinPool = new Pool<Interactable>(coinPrefab, transform, coinCount);
            fuelCanPool = new Pool<Interactable>(fuelCanPrefab, transform, fuelCanCount);

            activeItems = new List<Interactable>(staticCarCount + movingCarCount + coinCount + fuelCanCount);

            int lanes = roadSettings != null ? roadSettings.LaneCount : 3;
            previousMoving = new bool[lanes];
            currentMoving = new bool[lanes];

            rowSpacing = startRowSpacing;
            obstacleChance = startObstacleChance;

            if (player != null)
            {
                nextRowZ = player.position.z + firstRowDistance;
                nextFuelZ = player.position.z + Random.Range(fuelDistanceMin, fuelDistanceMax);
            }

            if (roadSettings != null)
            {
                lastSafeLane = roadSettings.CenterLane;
            }
        }

        private void Update()
        {
            if (player == null || roadSettings == null)
            {
                return;
            }

            while (nextRowZ < player.position.z + spawnDistance)
            {
                SpawnRow(nextRowZ);
                nextRowZ += RowSpacing;
            }

            float thresholdZ = player.position.z - releaseDistance;
            for (int i = activeItems.Count - 1; i >= 0; i--)
            {
                Interactable item = activeItems[i];
                if (item.IsUsed || item.transform.position.z < thresholdZ)
                {
                    ReleaseItem(item);
                    activeItems.RemoveAt(i);
                }
            }
        }

        private void SpawnRow(float rowZ)
        {
            int minLane = Mathf.Max(0, lastSafeLane - 1);
            int maxLane = Mathf.Min(roadSettings.LaneCount - 1, lastSafeLane + 1);

            int validCount = 0;
            for (int l = minLane; l <= maxLane; l++)
            {
                if (!previousMoving[l])
                {
                    validCount++;
                }
            }

            int chosenIndex = Random.Range(0, validCount);
            int safeLane = lastSafeLane;
            int counter = 0;
            for (int l = minLane; l <= maxLane; l++)
            {
                if (!previousMoving[l])
                {
                    if (counter == chosenIndex)
                    {
                        safeLane = l;
                        break;
                    }
                    counter++;
                }
            }
            lastSafeLane = safeLane;

            for (int lane = 0; lane < roadSettings.LaneCount; lane++)
            {
                if (lane == safeLane)
                {
                    if (rowZ >= nextFuelZ)
                    {
                        SpawnItem(fuelCanPool, fuelCanPrefab, safeLane, rowZ);
                        nextFuelZ += Random.Range(fuelDistanceMin, fuelDistanceMax);
                    }
                    else if (Random.value < coinChance)
                    {
                        for (int c = 0; c < coinsPerRow; c++)
                        {
                            float coinZ = rowZ + c * coinSpacing;
                            SpawnItem(coinPool, coinPrefab, safeLane, coinZ);
                        }
                    }
                }
                else
                {
                    if (previousMoving[lane])
                    {
                        if (Random.value < obstacleChance * movingCarChance)
                        {
                            SpawnItem(movingCarPool, movingCarPrefab, lane, rowZ);
                            currentMoving[lane] = true;
                        }
                    }
                    else
                    {
                        if (Random.value < obstacleChance)
                        {
                            if (Random.value < movingCarChance)
                            {
                                SpawnItem(movingCarPool, movingCarPrefab, lane, rowZ);
                                currentMoving[lane] = true;
                            }
                            else
                            {
                                SpawnItem(staticCarPool, staticCarPrefab, lane, rowZ);
                            }
                        }
                    }
                }
            }

            bool[] temp = previousMoving;
            previousMoving = currentMoving;
            currentMoving = temp;
            for (int i = 0; i < currentMoving.Length; i++)
            {
                currentMoving[i] = false;
            }
        }

        private void SpawnItem(Pool<Interactable> pool, Interactable prefab, int lane, float z)
        {
            if (pool == null || prefab == null)
            {
                return;
            }

            float x = roadSettings.GetLaneX(lane);
            float y = prefab.transform.position.y;
            Vector3 pos = new Vector3(x, y, z);

            Interactable item = pool.Get(pos, prefab.transform.rotation);
            if (item != null)
            {
                activeItems.Add(item);
            }
        }

        private void ReleaseItem(Interactable item)
        {
            if (item is StaticCar)
            {
                staticCarPool.Release(item);
            }
            else if (item is MovingCar)
            {
                movingCarPool.Release(item);
            }
            else if (item is Coin)
            {
                coinPool.Release(item);
            }
            else if (item is FuelCan)
            {
                fuelCanPool.Release(item);
            }
        }
    }
}
