// ====================================
// PLAYER INTERACTION (Módulo de Interacción)
// ====================================
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Configuración de Interacción")]
    public float interactionRange = 3f;
    public LayerMask interactableLayer = -1; // Todas las capas por defecto

    private Transform raycastOrigin;
    private IInteractable currentInteractable;

    public void Initialize(Transform cameraTransform)
    {
        raycastOrigin = cameraTransform;
    }

    public void CheckForInteractables()
    {
        if (raycastOrigin == null) return;

        RaycastHit hit;

        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, interactionRange, interactableLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (currentInteractable != interactable)
                {
                    currentInteractable = interactable;
                    OnInteractableFound(interactable);
                }
            }
            else
            {
                ClearCurrentInteractable();
            }
        }
        else
        {
            ClearCurrentInteractable();
        }
    }

    public void TryInteract()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    void OnInteractableFound(IInteractable interactable)
    {
        // Aquí puedes agregar lógica para mostrar UI, etc.
        Debug.Log($"Objeto interactuable encontrado: {interactable.GetInteractionText()}");
    }

    void ClearCurrentInteractable()
    {
        if (currentInteractable != null)
        {
            currentInteractable = null;
            Debug.Log("No hay objetos interactuables");
        }
    }

    // Métodos públicos para obtener información
    // Esto pregunta si estoy con un interactuable
    public bool HasInteractable() { return currentInteractable != null; }
    // Esto devuelve el texto que estoy recibiendo del interactuable
    public string GetCurrentInteractionText() { return currentInteractable?.GetInteractionText() ?? ""; }

    private void OnDrawGizmos()
    {
        if (raycastOrigin == null) return;

        Gizmos.color = Color.green;
        Vector3 start = raycastOrigin.position;
        Vector3 end = start + raycastOrigin.forward * interactionRange;
        Gizmos.DrawLine(start, end);
        Gizmos.DrawWireSphere(end, 0.05f); // pequeño indicador al final
    }
}