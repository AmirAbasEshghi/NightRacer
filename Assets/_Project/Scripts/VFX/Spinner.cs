using UnityEngine;

namespace NightRacer.VFX
{
    public class Spinner : MonoBehaviour
    {
        [SerializeField] private float spinSpeed = 180f;
        [SerializeField] private float bobHeight = 0.12f;
        [SerializeField] private float bobSpeed = 4f;

        private float baseY;
        private float phase;

        private void Awake()
        {
            baseY = transform.localPosition.y;
        }

        private void OnEnable()
        {
            phase = Random.Range(0f, Mathf.PI * 2f);
        }

        private void Update()
        {
            transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f, Space.Self);
            Vector3 pos = transform.localPosition;
            float newY = baseY + Mathf.Sin(Time.time * bobSpeed + phase) * bobHeight;
            transform.localPosition = new Vector3(pos.x, newY, pos.z);
        }
    }
}

