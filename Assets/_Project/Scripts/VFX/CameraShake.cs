using UnityEngine;

namespace NightRacer.VFX
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private float duration = 0.2f;
        [SerializeField] private float magnitude = 0.35f;

        private Vector3 initialLocalPosition;
        private float timer;

        private void Awake()
        {
            initialLocalPosition = transform.localPosition;
        }

        public void Shake()
        {
            timer = duration;
        }

        private void Update()
        {
            if (timer <= 0f)
            {
                return;
            }

            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer = 0f;
                transform.localPosition = initialLocalPosition;
                return;
            }

            Vector2 offset = Random.insideUnitCircle * magnitude;
            transform.localPosition = new Vector3(
                initialLocalPosition.x + offset.x,
                initialLocalPosition.y + offset.y,
                initialLocalPosition.z
            );
        }
    }
}

