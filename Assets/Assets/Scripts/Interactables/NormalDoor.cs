using UnityEngine;

public class NormalDoor : Interactable
{
    private bool open;
    public float smooth = 2.0f;
    public float DoorOpenAngle = 90.0f;

    private Vector3 defaultRot;
    private Vector3 openRot;

    void Start()
    {
        defaultRot = transform.eulerAngles;
        openRot = new Vector3(defaultRot.x, defaultRot.y + DoorOpenAngle, defaultRot.z);
        Debug.Log("Hola Elliot soy un ente de tu juego, no me sigas programando o te jalare las patas");
    }

    public override void Interact()
    {
        print("Interactuando con la puerta");
        open = !open; // solo alterna el estado
        if (open)
        {
            // sonido de abrir
            AudioManager.Instance.PlaySFX("OpenDoor");
        }
        else
        {
            // sonido de cerrar
            AudioManager.Instance.PlaySFX("CloseDoor");
        }
    }

    void Update()
    {
        Vector3 targetRot = open ? openRot : defaultRot;
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRot, Time.deltaTime * smooth);
    }
}
