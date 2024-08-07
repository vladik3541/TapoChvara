using System;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public event Action<int> OnChangeEnemy;
    [SerializeField] private GameObject[] enemys;
    private int levelEnemy;
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

    public GameObject Spawn()
    {
        if (levelEnemy >= enemys.Length) return null;
        currenEnemy = Instantiate(enemys[levelEnemy],new Vector3(10, 20,0), Quaternion.identity);
        levelEnemy++;
        OnChangeEnemy?.Invoke(levelEnemy);
        return currenEnemy;
    }
}
