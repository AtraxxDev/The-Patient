using Game;
using UnityEngine;
using System.Collections;
using static State;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 2f;
    [SerializeField] private Transform cameraTransform;

    [Header("PlayerSounds")]
    [SerializeField] private PlayerSounds playerSounds;

    private Rigidbody rb;
    private Vector2 moveInput;
    private float currentSpeed;

    private bool isMoving = false;
    private Coroutine footstepCoroutine;
    [SerializeField] private PlayerCrouch playerCrouch;

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

    public void SetMoving(bool moving)
    {
        if (isMoving == moving) return; // no hacer nada si no cambia el estado

        isMoving = moving;

        if (isMoving)
        {
            footstepCoroutine = StartCoroutine(FootstepRoutine());
        }
        else
        {
            if (footstepCoroutine != null)
                StopCoroutine(footstepCoroutine);
        }
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
        rb.MovePosition(rb.position + move * currentSpeed * Time.fixedDeltaTime);
    }

    private IEnumerator FootstepRoutine()
    {
        while (isMoving)
        {
            if (playerCrouch != null && playerCrouch.IsCrouching)
            {
                yield return null; // esperar siguiente frame
                continue;
            }

            // tiempos fijos
            float walkStepTime = 0.5f; // cada 0.5s al caminar
            float sprintStepTime = 0.2f; // cada 0.2s al correr

            bool isSprinting = currentSpeed > walkSpeed + 0.1f;
            float stepDelay = isSprinting ? sprintStepTime : walkStepTime;

            // reproducir audio
            AudioClip[] listofSteps = playerSounds.GetStepsAudioList();
            if (listofSteps.Length > 0)
            {
                AudioClip stepClip = listofSteps[Random.Range(0, listofSteps.Length)];
                playerSounds.ReturnPlayerStepsAudio().PlayOneShot(stepClip);
            }

            // esperar el tiempo de paso antes de continuar
            yield return new WaitForSeconds(stepDelay);
        }
    }
}
