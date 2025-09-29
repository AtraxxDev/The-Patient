using Game;
using UnityEngine;
using static State;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 2f;
    [SerializeField] private Transform cameraTransform;

    [Header("PlayerSounds")]
    [SerializeField] private PlayerSounds playerSounds;
    private float stepCooldown = 0.5f;   // time between steps
    private float lastStepTime = 0f;


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

    public void SetMoving(bool isMoving)
    {
        if (isMoving)
        {
            // Get how fast the player is actually moving in world space
            float velocity = rb.linearVelocity.magnitude;

            // Normalize speed ratio between 0 and 1
            float speedRatio = Mathf.Clamp01(velocity / (walkSpeed * sprintMultiplier));

            // Lerp step cooldown: slower when walking, faster when sprinting
            stepCooldown = Mathf.Lerp(0.6f, 0.25f, speedRatio);

            if (Time.time >= lastStepTime + stepCooldown)
            {
                AudioClip[] listofSteps = playerSounds.GetStepsAudioList();
                if (listofSteps.Length == 0) return;

                AudioClip stepClip = listofSteps[Random.Range(0, listofSteps.Length)];
                playerSounds.ReturnPlayerStepsAudio().PlayOneShot(stepClip);

                lastStepTime = Time.time;
            }
        }
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
