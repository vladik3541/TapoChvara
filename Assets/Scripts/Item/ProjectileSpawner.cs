using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] LeftPoint,RightPoint;
    List<GameObject> projectile;
    private UpgradeManager upgradeManager;

    private void Awake()
    {
        upgradeManager = FindObjectOfType<UpgradeManager>();
        projectile = new List<GameObject>();
    }
    private void OnEnable()
    {
        upgradeManager.OnUpgarade += AddProjectile;
        DamagePerClick.OnClickHit += Spawn;
    }
    private void OnDisable()
    {
        upgradeManager.OnUpgarade -= AddProjectile;
        DamagePerClick.OnClickHit -= Spawn;
    }

    public void AddProjectile(GameObject project)
    {
        if (!projectile.Contains(project) && project != null)
            projectile.Add(project);
    }
    private Vector3 GetRandomPosition()
    {
        // 0 Top
        // 1 Bot
        Vector3 randomPos;
        int randomSide = Random.Range(0, 2);
        if (randomSide == 0)
        {
            randomPos.y = Random.Range(LeftPoint[0].position.y, LeftPoint[1].position.y);
            randomPos.x = LeftPoint[0].position.x;
            randomPos.z = LeftPoint[0].position.z;
            return randomPos;
        }
        else
        {
            randomPos.y = Random.Range(RightPoint[0].position.y, RightPoint[1].position.y);
            randomPos.x = RightPoint[0].position.x;
            randomPos.z = RightPoint[0].position.z;
            return randomPos;
        }

    }
    private void Spawn(Vector3 target)
    {
        if (!projectile.Any()) return;

        GameObject project = Instantiate(projectile[Random.Range(0, projectile.Count)]);

        project.transform.position = GetRandomPosition();
        project.GetComponent<ProjectileController>().StartMovement(target);
    }

}
