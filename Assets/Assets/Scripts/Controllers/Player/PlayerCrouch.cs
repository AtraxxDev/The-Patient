using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private CapsuleCollider col;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private Transform cameraTransform; // cámara del jugador

    [Header("Configuración de Crouch")]
    [SerializeField] private float crouchHeight = 1f;
    [SerializeField] private float normalHeight = 2f;
    [SerializeField] private float crouchSpeed = 2f;
    [SerializeField] private float normalSpeed = 5f;
    [SerializeField] private float crouchTransitionSpeed = 5f;

    private bool isCrouching = false;
    private float targetHeight;
    private Vector3 originalCenter;
    private Vector3 originalCameraLocalPos;

    void Start()
    {
        targetHeight = normalHeight;
        col.height = normalHeight;
        originalCenter = col.center;
        originalCameraLocalPos = cameraTransform.localPosition;
    }

    void Update()
    {
        // Interpolación suave del collider
        col.height = Mathf.Lerp(col.height, targetHeight, Time.deltaTime * crouchTransitionSpeed);
        float heightDiff = col.height - normalHeight;
        col.center = originalCenter + new Vector3(0, heightDiff / 2f, 0);

        // Interpolación suave de la cámara
        Vector3 targetCamLocalPos = originalCameraLocalPos + new Vector3(0, heightDiff, 0);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, targetCamLocalPos, Time.deltaTime * crouchTransitionSpeed);
    }

    public void ToggleCrouch()
    {
        isCrouching = !isCrouching;
        targetHeight = isCrouching ? crouchHeight : normalHeight;
        movement.SetSpeed(isCrouching ? crouchSpeed : normalSpeed);
    }
}
