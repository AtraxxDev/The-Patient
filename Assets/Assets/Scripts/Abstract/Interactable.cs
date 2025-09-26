// ====================================
// EJEMPLO DE OBJETO INTERACTUABLE
// ====================================
using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    [Header("Configuración")]
    public string interactionText = "Press E to Iinteract";

    public abstract void Interact();
    public string GetInteractionText()
    {
        return interactionText;
    }
}