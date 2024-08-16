using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public const string KEY_GOLD = "Gold";
    public const string KEY_CURRENT_INDEX = "Enemy";
    public const string KEY_CURRENT_HEALTH = "Health";
    public void Initialize()
    {
        instance = this;
    }
    public void SaveGold(float value)
    {
        PlayerPrefs.SetFloat(KEY_GOLD, value);
        PlayerPrefs.Save();
    }
    public float GetSaveGold()
    {
        return PlayerPrefs.GetFloat(KEY_GOLD);
    }

    public void SaveEnemyCurrentIndex(int currentIndex)
    {
        PlayerPrefs.SetInt(KEY_CURRENT_INDEX, currentIndex);
        PlayerPrefs.Save();
    }
    public void SaveEnemyCurrentHealth( float currentHealth)
    {
        PlayerPrefs.SetFloat(KEY_CURRENT_HEALTH, currentHealth);
        PlayerPrefs.Save();
    }
    public int GetCurrentIndex()
    {
        return PlayerPrefs.GetInt(KEY_CURRENT_INDEX);
    }
    public float GetCurrentHealth()
    {
        return PlayerPrefs.GetFloat(KEY_CURRENT_HEALTH);
    }
    
}
