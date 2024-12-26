using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PoolGround))]
public class GroundManager : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float distanceBetween;
    [SerializeField] private GameObject[] _ground;
    [SerializeField] private int levelForSwitchGround, levelForSwitchSand;

    const int levelGround = 1, levelSand = 2;

    private PoolGround poolGround;
    private Transform lastGround;
    private const int GROUND_COUNT = 5;
    private float currentSpeed;

    private int _level = 0;
    public float Speed { get { return speed;} }

    private SpawnEnemy spawnEnemy;

    private void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        currentSpeed = speed;
        poolGround = GetComponent<PoolGround>();
        spawnEnemy = FindObjectOfType<SpawnEnemy>();
        spawnEnemy.OnChangeEnemy += ChangeLevelPlace;
        if (PlayerPrefs.HasKey("GroundLevel"))
        {
            _level = PlayerPrefs.GetInt("GroundLevel");
        }
        poolGround.CreateInitialPool(_ground[_level]);
        StartSpawm();
    }
    public void StopedMove(bool value)
    {
        if(value)
        {
            speed = 0;
        }
        else
        {
            speed = currentSpeed;
        }
    }
    private void StartSpawm()
    {
        for (int i = 0; i < GROUND_COUNT; i++)// new
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject ground = poolGround.GetObjectFromPool();
        ground.transform.parent = transform;
        Vector3 pointSpawn = Vector3.zero;
        if (lastGround==null)
        {
            pointSpawn.z = 0;
        }
        else
        {
            pointSpawn.z = lastGround.position.z + distanceBetween;
        }
        ground.transform.position = pointSpawn;

        lastGround = ground.transform;
    }
    public void Remove(GameObject ground)
    {
        Spawn();
        ground.transform.position = Vector3.zero;
        poolGround.ReturnObjectToPool(ground);
    }
    private void ChangeLevelPlace(int level)
    {

        lastGround = null;
        if (level == levelForSwitchGround)
        {
            _level = levelGround;
            poolGround.RecreatePool(_ground[_level]);
            StartSpawm();
        }
        else if (level == levelForSwitchSand)
        {
            _level = levelSand;
            poolGround.RecreatePool(_ground[_level]);
            StartSpawm();
        }
        PlayerPrefs.SetInt("GroundLevel", _level);
        PlayerPrefs.Save();
    }
    private void OnDisable()
    {
        spawnEnemy.OnChangeEnemy -= ChangeLevelPlace;
    }
}
