// ====================================
// EJEMPLO DE OBJETO INTERACTUABLE
// ====================================
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [Header("Configuración")]
    public string interactionText = "Press E to Iinteract";

    public void Interact()
    {
        Debug.Log($"Interactuando con {gameObject.name}");
        // Aquí va tu lógica específica
    }

    public string GetInteractionText()
    {
        return interactionText;
    }
}