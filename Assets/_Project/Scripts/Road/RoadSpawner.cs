using System.Collections.Generic;
using UnityEngine;
using NightRacer.Pooling;

namespace NightRacer.Road
{
    public class RoadSpawner : MonoBehaviour
    {
        [SerializeField] private RoadSettings roadSettings;
        [SerializeField] private Transform segmentPrefab;
        [SerializeField] private Transform player;

        private Pool<Transform> pool;
        private Queue<Transform> activeSegments;
        private float nextSpawnZ;

        private void Awake()
        {
            pool = new Pool<Transform>(segmentPrefab, transform, roadSettings.SegmentCount);
            activeSegments = new Queue<Transform>(roadSettings.SegmentCount);

            float startZ = -roadSettings.SegmentLength;
            for (int i = 0; i < roadSettings.SegmentCount; i++)
            {
                float z = startZ + i * roadSettings.SegmentLength;
                Transform segment = pool.Get(new Vector3(0f, 0f, z), Quaternion.identity);
                activeSegments.Enqueue(segment);
                nextSpawnZ = z;
            }
        }

        private void Update()
        {
            if (player == null || activeSegments.Count == 0)
            {
                return;
            }

            float recycleZ = player.position.z - roadSettings.RecycleDistance;
            float halfLength = roadSettings.SegmentLength * 0.5f;

            while (activeSegments.Count > 0 && activeSegments.Peek().position.z + halfLength < recycleZ)
            {
                Transform oldest = activeSegments.Dequeue();
                pool.Release(oldest);

                nextSpawnZ += roadSettings.SegmentLength;
                Transform next = pool.Get(new Vector3(0f, 0f, nextSpawnZ), Quaternion.identity);
                if (next != null)
                {
                    activeSegments.Enqueue(next);
                }
            }
        }
    }
}

