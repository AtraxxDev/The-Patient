using System;
using UnityEngine;

public class Heroine : Interactable
{
    public event Action OnHeroineCollected;
    public override void Interact()
    {
        gameObject.SetActive(false);
        OnHeroineCollected?.Invoke();
    }

    
}
