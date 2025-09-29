using Game;
using UnityEngine;

public class CreateAttack : MonoBehaviour
{
    [SerializeField]
    LayerMask CharactersLayers = Physics.DefaultRaycastLayers;

    

    public void MakeAttack(NoiseInfo noise)
    {
        var colliders = Physics.OverlapSphere(noise.position, noise.Radius, CharactersLayers, QueryTriggerInteraction.Ignore);

        foreach (var collider in colliders)
        {

            if (collider.TryGetComponent<PlayerDeath>(out PlayerDeath playerDeath))
            {
                playerDeath.YouAreDead();
            }
        }
    }
}
