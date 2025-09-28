using UnityEngine;
using System.Collections;

public class BlinkEffect : MonoBehaviour
{
    [Header("Blink")]
    [SerializeField] private float blinkSpeed = 2f;
    [SerializeField] private Color minEmission = new Color(0f, 0f, 0f);            // 0x000000
    [SerializeField] private Color maxEmission = new Color(0.286f, 0.286f, 0.286f); // 0x494949
    [SerializeField] private float intensityMultiplier = 3f; // multiplicador para que se vea

    private MeshRenderer meshRenderer;
    private Material mat;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            Debug.LogError("BlinkEffectHDRP: no MeshRenderer");
            enabled = false;
            return;
        }

        // crear instancia (no modifica sharedMaterial)
        mat = meshRenderer.material;
        if (mat == null)
        {
            Debug.LogError("BlinkEffectHDRP: material null");
            enabled = false;
            return;
        }

        // Habilitar keywords comunes
        if (mat.HasProperty("_EmissionColor"))
        {
            mat.EnableKeyword("_EMISSION");
            Debug.Log($"BlinkEffect: material tiene _EmissionColor");
        }
        if (mat.HasProperty("_EmissiveColor"))
        {
            mat.EnableKeyword("_EMISSIVE_COLOR");
            Debug.Log($"BlinkEffect: material tiene _EmissiveColor");
        }

        // Permitir que la emisión actúe en tiempo real (opcional)
        mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;

        // start
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        while (true)
        {
            float t = (Mathf.Sin(Time.time * blinkSpeed * Mathf.PI * 2f) + 1f) / 2f;
            Color target = Color.Lerp(minEmission, maxEmission, t) * intensityMultiplier;

            // Intenta varias propiedades (fallbacks)
            if (mat.HasProperty("_EmissionColor"))
                mat.SetColor("_EmissionColor", target);

            if (mat.HasProperty("_EmissiveColor"))
                mat.SetColor("_EmissiveColor", target);

            // Algunos shaders tienen intensidad en una propiedad separada
            if (mat.HasProperty("_EmissiveIntensity"))
                mat.SetFloat("_EmissiveIntensity", target.maxColorComponent);

            yield return null;
        }
    }

    private void OnDestroy()
    {
        // opcional: restaurar valor si quieres
        if (mat != null)
        {
            if (mat.HasProperty("_EmissionColor")) mat.SetColor("_EmissionColor", Color.black);
            if (mat.HasProperty("_EmissiveColor")) mat.SetColor("_EmissiveColor", Color.black);
        }
    }
}
