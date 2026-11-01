using UnityEngine;
using NightRacer.Player;

namespace NightRacer.VFX
{
    public class NeonUnderglow : MonoBehaviour
    {
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private Light[] neonLights;
        [SerializeField] private Renderer glowRenderer;
        [SerializeField] [ColorUsage(true, true)] private Color neonColor = new Color(0f, 0.9f, 1f, 1f);
        [SerializeField] private float minSpeed = 20f;
        [SerializeField] private float maxSpeed = 60f;
        [SerializeField] private float minLightIntensity = 1f;
        [SerializeField] private float maxLightIntensity = 5f;
        [SerializeField] private float minEmission = 1f;
        [SerializeField] private float maxEmission = 4f;

        private static readonly int EmissionColorId = Shader.PropertyToID("_EmissionColor");
        private MaterialPropertyBlock propBlock;

        private void Awake()
        {
            propBlock = new MaterialPropertyBlock();
        }

        private void Update()
        {
            if (movement == null)
            {
                return;
            }

            float t = Mathf.InverseLerp(minSpeed, maxSpeed, movement.Speed);
            float lightIntensity = Mathf.Lerp(minLightIntensity, maxLightIntensity, t);
            float emissionIntensity = Mathf.Lerp(minEmission, maxEmission, t);

            if (neonLights != null)
            {
                for (int i = 0; i < neonLights.Length; i++)
                {
                    if (neonLights[i] != null)
                    {
                        neonLights[i].intensity = lightIntensity;
                    }
                }
            }

            if (glowRenderer != null)
            {
                glowRenderer.GetPropertyBlock(propBlock);
                propBlock.SetColor(EmissionColorId, neonColor * emissionIntensity);
                glowRenderer.SetPropertyBlock(propBlock);
            }
        }
    }
}

