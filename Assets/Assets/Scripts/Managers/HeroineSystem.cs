using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HeroineSystem : MonoBehaviour
{
    [SerializeField] private List<Heroine> heroineList;
    [SerializeField] private float amount = 0;

    [SerializeField] private float corruptionPercentage;

    public event Action OnHeroineUse;
    public event Action OnHeroineRemove;

    private void OnEnable()
    {
        for (int i = 0; i < heroineList.Count; i++)
        {
            Heroine heroines = heroineList[i];
            heroines.OnHeroineCollected += AddAmount; 
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < heroineList.Count; i++)
        {
            Heroine heroines = heroineList[i];
            heroines.OnHeroineCollected -= AddAmount;
        }
    }

    private void AddAmount()
    {
        amount ++;
        OnHeroineUse?.Invoke();
        Debug.Log($"Heroíne Collected. Total: {amount}");

    }
    private void RemoveAmount()
    {
        amount--;
        OnHeroineRemove?.Invoke();
        Debug.Log($"Heroíne Used. Total: {amount}");

    }

    public void UseHeroine()
    {
        if (amount <= 0)
        {
            print("You dont have more heroine to consume");
            return;
        }
        RemoveAmount();
        corruptionPercentage = Mathf.Clamp(corruptionPercentage + 15, 0, 100);
        print($"You consumed 1 of heroine {corruptionPercentage}%");
    }
}
