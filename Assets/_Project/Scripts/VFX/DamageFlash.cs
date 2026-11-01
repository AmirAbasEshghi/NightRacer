using UnityEngine;

namespace NightRacer.VFX
{
    public class DamageFlash : MonoBehaviour
    {
        [SerializeField] private Transform body;
        [SerializeField] private Color flashColor = new Color(2f, 0.2f, 0.2f, 1f);
        [SerializeField] private float duration = 0.2f;

        private static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");

        private Renderer[] renderers;
        private MaterialPropertyBlock propBlock;
        private float timer;
        private bool isFlashing;

        private void Awake()
        {
            propBlock = new MaterialPropertyBlock();
            if (body != null)
            {
                renderers = body.GetComponentsInChildren<Renderer>(true);
            }
            else
            {
                renderers = GetComponentsInChildren<Renderer>(true);
            }
        }

        public void Flash()
        {
            timer = duration;
            isFlashing = true;
            ApplyColor(flashColor);
        }

        private void Update()
        {
            if (!isFlashing)
            {
                return;
            }

            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer = 0f;
                isFlashing = false;
                ApplyColor(Color.white);
            }
        }

        private void ApplyColor(Color color)
        {
            if (renderers == null)
            {
                return;
            }

            propBlock.SetColor(BaseColorId, color);
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].SetPropertyBlock(propBlock);
            }
        }
    }
}

