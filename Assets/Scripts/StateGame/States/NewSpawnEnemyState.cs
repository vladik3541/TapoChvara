using UnityEngine;
using DG.Tweening;

public class NewSpawnEnemyState : State
{
    private GameStateManager stateManager;
    private float duration = 1;

    public NewSpawnEnemyState(GameStateManager gameStateManager) 
    {
        stateManager = gameStateManager;    
    }
    public override void Enter()
    {
        stateManager.isGame = true;
        GameObject enemy = stateManager.enemyManager.Spawn();
        stateManager.uiManager.UpdateLevel(stateManager.enemyManager.LevelEnemy);
        StartBehavior(enemy);
    }

    public override void Exit()
    {
        stateManager.groundManager.StopedMove(false);
    }

    public override void Update()
    {
        
    }
    private void StartBehavior(GameObject enemy)
    {
        enemy.GetComponent<Animator>().enabled = false;
        enemy.transform.DOMove(Vector3.zero, duration)
            .SetEase(Ease.Flash)
            .OnComplete(() =>
            {
                enemy.GetComponent<Animator>().enabled = true;
                enemy.GetComponent<Enemy>().PlayDustEffect();
                stateManager.groundManager.StopedMove(true);
            });
    }
}
