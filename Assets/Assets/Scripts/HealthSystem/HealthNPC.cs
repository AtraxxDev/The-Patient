using UnityEngine;

public class HealthNPC : MonoBehaviour
{
    [SerializeField] private float _health=100f;

    public void TakeDamage(float Damage)
    {
        _health -= Damage;
        if (_health <= 0)
        {
           //You are Dead
           if(TryGetComponent<PlayerDeath>(out var playerDeath))
            {
                playerDeath.YouAreDead();
            }

           if(TryGetComponent<DeathNPC>(out var deathNPC))
            {
                deathNPC.IAmDead();
            }

        }
    }
}
