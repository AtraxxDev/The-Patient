using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Referencias")]
    public Transform playerBody; // el objeto Player
    public float mouseSensitivity = 100f;

    private float xRotation = 0f;

    // Este método recibe el input desde PlayerController
    public void HandleLook(Vector2 lookInput)
    {
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // Rotación vertical de la cámara
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -75f, 75f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotación horizontal del cuerpo del jugador
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
