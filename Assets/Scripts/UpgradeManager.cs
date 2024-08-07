using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public event Action<GameObject> OnUpgarade;
    public static event Action upgrade;
    [SerializeField] private List<Ability> upgradesList;
    private Dictionary<string, int> upgradesLevels;
    private int totalDamage;

    private void Start()
    {
        upgradesLevels = new Dictionary<string, int>();

        // Ініціалізація рівнів апгрейдів
        foreach (Ability upgrade in upgradesList)
        {
            upgradesLevels[upgrade.name] = 0; // Початковий рівень апгрейду
        }
    }
    public void UpgradeElement(Ability ability)
    {
        if (upgradesLevels.ContainsKey(ability.Name) && GoldManager.instance.RemoveGold(ability.Price))
        {
            upgradesLevels[ability.Name]++;
            OnUpgarade?.Invoke(ability.Projectile);
            upgrade?.Invoke();
            Debug.Log($"{ability.Name} upgraded to level {upgradesLevels[ability.Name]}");
        }
        else
        {
            Debug.LogWarning($"Upgrade {ability.Name} does not exist in the dictionary.");
        }
    }
    public int GetUpgradeLevel(Ability ability)
    {
        if (upgradesLevels.ContainsKey(ability.Name))
        {
            return upgradesLevels[ability.Name];
        }
        else
        {
            Debug.LogWarning($"Upgrade {ability.Name} does not exist in the dictionary.");
            return 0;
        }
    }
    public int GetAllDamagePerClick()
    {
        totalDamage = 0;
        foreach (Ability ability in upgradesList)
        {
            if(!ability.IsDamagePerSeconde)
                totalDamage += GetUpgradeLevel(ability) * ability.Damage;
        }
        return totalDamage;
    }
    public int GetAllDamagePerSeconds()
    {
        totalDamage = 0;
        foreach (Ability ability in upgradesList)
        {
            if (ability.IsDamagePerSeconde)
                totalDamage += GetUpgradeLevel(ability) * ability.Damage;
        }
        return totalDamage;
    }

}
