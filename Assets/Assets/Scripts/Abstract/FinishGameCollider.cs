using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FinishGameCollider : MonoBehaviour
{
    [SerializeField] private LayerMask charactersLayers = Physics.DefaultRaycastLayers;

    [SerializeField] private HeroineSystem heroineSystems;
    [SerializeField] private Canvas endgameCanvas;
    [SerializeField] private GameObject[] playerText; // 0 = Final Bueno, 1 = Final Malo, 2 = Final Muy Malo

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object's layer is part of CharactersLayers
        if (((1 << other.gameObject.layer) & charactersLayers) != 0)
        {
            if (other.TryGetComponent<PlayerDeath>(out PlayerDeath playerDeath))
            {

                if (endgameCanvas != null)
                    endgameCanvas.enabled = true;

           

                if (heroineSystems.KarmaAmount >= 5)
                {
                    playerText[2].gameObject.active = true; // Final Muy Malo
                }
                else if (heroineSystems.KarmaAmount >= 1)
                {
                    playerText[1].gameObject.active = true; // Final Malo
                }
                else // KarmaAmount == 0
                {
                    playerText[0].gameObject.active = true; // Final Bueno
                }

            }
        }
    }
}
