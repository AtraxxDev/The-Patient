using Game;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 2f;
    [SerializeField] private Transform cameraTransform;

    private Rigidbody rb;
    private Vector2 moveInput;
    private float currentSpeed;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        currentSpeed = walkSpeed;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    public void SetSpeed(float speed)
    {
        currentSpeed = speed; // usado por crouch
    }

    public void SetSprinting(bool isSprinting)
    {
        currentSpeed = isSprinting ? walkSpeed * sprintMultiplier : walkSpeed;
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
        //noiseMaker.MakeNoise(NoiseInfo);

        rb.MovePosition(rb.position + move * currentSpeed * Time.fixedDeltaTime);
    }


}
