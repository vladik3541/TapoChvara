using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public event Action<GameObject> OnUpgarade;
    public static event Action upgrade;
    [SerializeField] private List<Ability> upgradesList;
    private List<int> countUpgradeLevel;
    private int totalDamage;
    private AbilittiCell[] abilittiCells;

    private void Start()
    {
        abilittiCells = FindObjectsOfType<AbilittiCell>();
        countUpgradeLevel = new List<int>();
        if(SaveManager.instance.GetAbilityList(out List<int> _list))
        {
            countUpgradeLevel = _list;
            foreach(AbilittiCell cell in abilittiCells)
            {
                cell.UpdateTextUpgrade();
            }
            Debug.Log("Success");
        }
        else
        {
            // Ініціалізація рівнів апгрейдів
            for (int i = 0; i < upgradesList.Count; i++)
            {
                countUpgradeLevel.Add(0);
            }
        }
    }
    public void UpgradeElement(Ability ability)
    {
        if (GoldManager.instance.RemoveGold(ability.Price))
        {
            for (int i = 0; i < countUpgradeLevel.Count; i++)
            {
                if(ability.name == upgradesList[i].name)
                {
                    countUpgradeLevel[i]++;
                }
            }
            SaveManager.instance.SaveAbility(countUpgradeLevel);
            OnUpgarade?.Invoke(ability.Projectile);
            upgrade?.Invoke();
            Debug.Log($"{ability.Name} upgraded to level");
        }
        else
        {
            Debug.LogWarning($"Upgrade {ability.Name} does not exist in the dictionary.");
        }
    }
    public int GetUpgradeLevel(Ability ability)
    {
        for (int i = 0; i < upgradesList.Count; i++)
        {
            if (ability.name == upgradesList[i].name)
            {
                return countUpgradeLevel[i];
            }
        }
        Debug.LogWarning($"Upgrade {ability.Name} does not exist in the dictionary.");
        return 0;
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
