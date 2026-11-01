using UnityEngine;

namespace NightRacer.Road
{
    [CreateAssetMenu(menuName = "NightRacer/Road Settings")]
    public class RoadSettings : ScriptableObject
    {
        [SerializeField] private int laneCount = 3;
        [SerializeField] private float laneWidth = 3f;
        [SerializeField] private float segmentLength = 20f;
        [SerializeField] private int segmentCount = 12;
        [SerializeField] private float recycleDistance = 30f;

        public int LaneCount => laneCount;
        public float LaneWidth => laneWidth;
        public float SegmentLength => segmentLength;
        public int SegmentCount => segmentCount;
        public float RecycleDistance => recycleDistance;
        public int CenterLane => laneCount / 2;

        public float GetLaneX(int lane)
        {
            float half = (laneCount - 1) * 0.5f;
            return (lane - half) * laneWidth;
        }
    }
}

