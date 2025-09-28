using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class CorruptionSystem : MonoBehaviour
{
    [SerializeField] private HeroineSystem heroineSystem;

    [Title("Datos")]
    public StateCorruptionType corruptionType = StateCorruptionType.Normal;

    [SerializeField] private float corruptionPercentage;

    public event Action<StateCorruptionType> OnCorruptionChanged;

    private void OnEnable()
    {
        heroineSystem.OnHeroineConsumed += IncrementCorruption;
    }

    private void OnDisable()
    {
        heroineSystem.OnHeroineConsumed -= IncrementCorruption;

    }

    public void IncrementCorruption()
    {
        corruptionPercentage = Mathf.Clamp(corruptionPercentage + 15, 0, 100);
        UpdateCorruptionType();
        print($"Corruption: {corruptionPercentage}%");
    }

    private void UpdateCorruptionType()
    {
        if (corruptionPercentage == 0)
        {
            corruptionType = StateCorruptionType.Normal;
        }
        else if (corruptionPercentage > 0 && corruptionPercentage <= 25)
        {
            corruptionType = StateCorruptionType.Distortion;
        }
        else if (corruptionPercentage > 25 && corruptionPercentage <= 75)
        {
            corruptionType = StateCorruptionType.Corrupt;
        }
        else // >75 hasta 100
        {
            corruptionType = StateCorruptionType.Untied;
            
        }

        OnCorruptionChanged?.Invoke(corruptionType);
    }
}

public enum StateCorruptionType
{
    Normal,
    Distortion,
    Corrupt,
    Untied
}
