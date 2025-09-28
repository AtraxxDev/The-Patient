using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class CorruptionPostProcess : MonoBehaviour
{
    [SerializeField] private CorruptionSystem corruptionSystem; // tu script de corrupción
    [SerializeField] private Volume volume; // tu Volume HDRP

    private Vignette vignette;
    private LensDistortion lensDistortion;
    private ChromaticAberration chromaticAberration;

    [Title("Visual Elements")]
    [SerializeField] private List<GameObject> enemiesList = new List<GameObject>();
    [SerializeField] private MeshRenderer meshPlaneAbeeration;
    [SerializeField] private Material material_X_Ray;

    private void Start()
    {
        // Obtener los componentes del Volume
        volume.profile.TryGet<Vignette>(out vignette);
        volume.profile.TryGet<LensDistortion>(out lensDistortion);
        volume.profile.TryGet<ChromaticAberration>(out chromaticAberration);

        // Inicializar según estado actual
        UpdatePostProcess(corruptionSystem.corruptionType);

        // Suscribirse a cambios
        corruptionSystem.OnCorruptionChanged += UpdatePostProcess;
    }

    private void OnDestroy()
    {
        corruptionSystem.OnCorruptionChanged -= UpdatePostProcess;
    }

    private void UpdatePostProcess(StateCorruptionType type)
    {
        // Desactivar todos primero
        if (vignette != null) vignette.active = false;
        if (lensDistortion != null) lensDistortion.active = false;
        if (chromaticAberration != null) chromaticAberration.active = false;

        // Activar según tipo de corrupción
        switch (type)
        {
            case StateCorruptionType.Normal:
                break;
            case StateCorruptionType.Distortion:
                if (vignette != null) vignette.active = true;
                break;
            case StateCorruptionType.Corrupt:
                if (vignette != null) vignette.active = true;
                if (lensDistortion != null) lensDistortion.active = true;
                break;
            case StateCorruptionType.Untied:
                if (vignette != null) vignette.active = true;
                if (lensDistortion != null) lensDistortion.active = true;
                if (chromaticAberration != null) chromaticAberration.active = true;
                meshPlaneAbeeration.enabled = true;
                foreach (var enemy in enemiesList)
                {
                    // Buscar el hijo llamado "kidney"
                    Transform kidneyParent = enemy.transform.Find("Kidney");
                    if (kidneyParent == null) continue;

                    // Recorrer todos los hijos de "kidney" (los dos riñones)
                    foreach (Transform kidney in kidneyParent)
                    {
                        MeshRenderer mesh = kidney.GetComponent<MeshRenderer>();
                        if (mesh != null)
                        {
                            // Agregar el material al final de los existentes
                            List<Material> mats = new List<Material>(mesh.materials);
                            mats.Add(material_X_Ray);
                            mesh.materials = mats.ToArray();
                        }
                    }
                }


                break;
        }
    }
}
