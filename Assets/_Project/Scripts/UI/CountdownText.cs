using UnityEngine;
using TMPro;
using NightRacer.Core;

namespace NightRacer.UI
{
    public class CountdownText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        private void Start()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnCountdownTick += HandleTick;
                GameManager.Instance.OnStateChanged += HandleStateChanged;
            }

            if (text != null)
            {
                text.enabled = false;
            }
        }

        private void OnDestroy()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnCountdownTick -= HandleTick;
                GameManager.Instance.OnStateChanged -= HandleStateChanged;
            }
        }

        private void HandleTick(int count)
        {
            if (text != null)
            {
                text.SetText("{0}", count);
                text.enabled = true;
            }
        }

        private void HandleStateChanged(GameState state)
        {
            if (state != GameState.Resuming && text != null)
            {
                text.enabled = false;
            }
        }
    }
}
