using System;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public event Action<int> OnChangeEnemy;
    [SerializeField] private GameObject[] enemys;
    private int levelEnemy;
    private float health;
    public int LevelEnemy
    {
        get { return levelEnemy; }
    }
    private GameObject currenEnemy;
    public GameObject CurrentEnemy
    {
        get
        {
            return currenEnemy;
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(SaveManager.KEY_CURRENT_HEALTH))
            health = SaveManager.instance.GetCurrentHealth();
        if (PlayerPrefs.HasKey(SaveManager.KEY_CURRENT_INDEX))
            levelEnemy = SaveManager.instance.GetCurrentIndex();
    }
    public GameObject Spawn()
    {
        if (levelEnemy >= enemys.Length) return null;
        currenEnemy = Instantiate(enemys[levelEnemy],new Vector3(10, 20,0), Quaternion.identity);
        levelEnemy++;
        OnChangeEnemy?.Invoke(levelEnemy);
        SaveManager.instance.SaveEnemyCurrentIndex(levelEnemy);
        return currenEnemy;
    }
}
