using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public Transform cameraTransform;


    private Rigidbody rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
    }

    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    private void Move()
    {
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 move = camForward * moveInput.y + camRight * moveInput.x;

        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }
}
