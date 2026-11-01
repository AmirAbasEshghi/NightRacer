using UnityEngine;

namespace NightRacer.Player
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new Vector3(0f, 3.5f, -7f);
        [SerializeField, Range(0f, 1f)] private float xFollow = 0.6f;
        [SerializeField] private float followSpeed = 8f;

        private void LateUpdate()
        {
            if (target == null)
            {
                return;
            }

            float desiredX = target.position.x * xFollow + offset.x;
            float currentX = Mathf.Lerp(transform.position.x, desiredX, followSpeed * Time.deltaTime);

            transform.position = new Vector3(currentX, target.position.y + offset.y, target.position.z + offset.z);
        }
    }
}

