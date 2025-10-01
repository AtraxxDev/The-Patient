using UnityEngine;

public class DeathNPC : MonoBehaviour
{
    public void IAmDead()
    {
        gameObject.SetActive(false);
    }
}
