using Game;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("M�dulos")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private PlayerCrouch playerCrouch;
    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private HeroineSystem heroineSystem;
    [SerializeField] private NoiseMaker noiseMaker;

    [Header("Configuraci�n Global")]
    public bool enableMovement = true;
    public bool enableInteraction = true;
    public bool enableMouseControl = true;


    private void Start()
    {
        playerInteraction.Initialize(playerCamera.transform);
    }

    void Update()
    {
        HandleInteraction();
    }



    void HandleInteraction()
    {
        if (!enableInteraction || playerInteraction == null) return;

        playerInteraction.CheckForInteractables();
    }

    // ======== INPUT SYSTEM (vinculado desde PlayerInput) ========

    public void OnMove(InputValue value)
    {
        if (enableMovement && playerMovement != null)
            playerMovement.SetMoveInput(value.Get<Vector2>());

        if(!playerCrouch)
        noiseMaker.MakeNoise(new NoiseInfo
        {
            position = this.transform.position,
            Radius = 15f,
            type = NoiseType.Common
            
        });
        
    }

    public void OnSprint(InputValue value)
    {
        // si est� agachado, no puede sprintar
        if (playerCrouch != null && playerCrouch.IsCrouching)
        {
            return;
        }

        playerMovement.SetSprinting(value.isPressed);
        noiseMaker.MakeNoise(new NoiseInfo
        {
            position = this.transform.position,
            Radius = 25f,
            type = NoiseType.Common

        });
    }

    public void OnUse(InputValue value)
    {
        heroineSystem.UseHeroine();
    }



    public void OnCrouch(InputValue value)
    {
        playerCrouch.ToggleCrouch();
        noiseMaker.MakeNoise(new NoiseInfo
        {
            position = this.transform.position,
            Radius = 5f,
            type = NoiseType.Common

        });

    }

    public void OnInteract(InputValue value)
    {
        if (enableInteraction && playerInteraction != null && value.isPressed)
            playerInteraction.TryInteract();
    }

    public void OnLook(InputValue value)
    {
        if (enableMouseControl && playerCamera != null)
            playerCamera.HandleLook(value.Get<Vector2>()); // pasa input al m�dulo de la c�mara
    }


    //Da un aviso para que pueda hacer ruido
    public void OnMakeNoise(InputValue value)
    {
        noiseMaker.MakeNoise(new NoiseInfo
        {
            position = this.transform.position,
            Radius = 35f,
            type = NoiseType.Common

        });
    }

    // ======== M�todos p�blicos para habilitar m�dulos ========
    public void EnableMovement(bool enable) => enableMovement = enable;
    public void EnableInteraction(bool enable) => enableInteraction = enable;
    public void EnableMouseControl(bool enable) => enableMouseControl = enable;
    public void EnableAllModules(bool enable) => enableMovement = enableInteraction = enableMouseControl = enable;
}
