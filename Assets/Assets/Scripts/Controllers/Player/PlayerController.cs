using Game;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("Módulos")]
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private PlayerInteraction playerInteraction;
    [SerializeField]
    private PlayerCamera playerCamera;
    [SerializeField]
    NoiseMaker noiseMaker;

    [Header("Configuración Global")]
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

        noiseMaker.MakeNoise(new NoiseInfo
        {
            position = this.transform.position,
            Radius = 15f,
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
            playerCamera.HandleLook(value.Get<Vector2>()); // pasa input al módulo de la cámara
    }


    //Da un aviso para que pueda hacer ruido
    public void OnMakeNoise(InputValue value)
    {
        noiseMaker.MakeNoise(new NoiseInfo
        {
            position = this.transform.position,
            Radius = 10f,
            type = NoiseType.Common

        });
    }

    // ======== Métodos públicos para habilitar módulos ========
    public void EnableMovement(bool enable) => enableMovement = enable;
    public void EnableInteraction(bool enable) => enableInteraction = enable;
    public void EnableMouseControl(bool enable) => enableMouseControl = enable;
    public void EnableAllModules(bool enable) => enableMovement = enableInteraction = enableMouseControl = enable;
}
