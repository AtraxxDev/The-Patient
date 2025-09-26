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
    }

    public override void Interact()
    {
        print("Interactuando con la puerta");
        open = !open; // solo alterna el estado
    }

    void Update()
    {
        Vector3 targetRot = open ? openRot : defaultRot;
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRot, Time.deltaTime * smooth);
    }
}
