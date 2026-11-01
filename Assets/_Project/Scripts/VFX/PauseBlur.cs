using UnityEngine;
using UnityEngine.Rendering;
using NightRacer.Core;

namespace NightRacer.VFX
{
    public class PauseBlur : MonoBehaviour
    {
        [SerializeField] private Volume volume;

        private void Start()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged += HandleStateChanged;
                HandleStateChanged(GameManager.Instance.State);
            }
        }

        private void OnDestroy()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged -= HandleStateChanged;
            }
        }

        private void HandleStateChanged(GameState state)
        {
            if (volume != null)
            {
                volume.weight = (state == GameState.Paused) ? 1f : 0f;
            }
        }
    }
}

