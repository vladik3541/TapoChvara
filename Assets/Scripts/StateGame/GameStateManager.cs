using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    [HideInInspector] public StateMachine stateMachine;
    [HideInInspector] public NewSpawnEnemyState newSpawnEnemyState;
    [HideInInspector] public GamePlayState gamePlayState;
    [HideInInspector] public EnemyDieState enemyDieState;

    [HideInInspector] public GroundManager groundManager;
    [HideInInspector] public SpawnEnemy enemyManager;
    [HideInInspector] public UIManager uiManager;

    public bool isGame;
    public void Initialize()
    {
        instance = this;

        enemyManager = FindObjectOfType<SpawnEnemy>();
        groundManager = FindObjectOfType<GroundManager>();
        uiManager = FindObjectOfType<UIManager>();

        stateMachine = new StateMachine();

        newSpawnEnemyState = new NewSpawnEnemyState(this);
        gamePlayState = new GamePlayState(this);
        enemyDieState = new EnemyDieState(this);

        stateMachine.InitializeState(newSpawnEnemyState);
    }
    private void OnEnable()
    {
        Enemy.OnRun += StartGame;
        EnemyHealth.OnDeath += EnemyDie;
        enemyDieState.OnEndDie += SpawnNewEnemy;
    }
    private void OnDisable()
    {
        Enemy.OnRun -= StartGame;
        EnemyHealth.OnDeath -= EnemyDie;
        enemyDieState.OnEndDie -= SpawnNewEnemy;
    }
    public void StartGame() // invoke when enemy lets strat run
    {
        stateMachine.SwitchState(gamePlayState);
    }
    public void EnemyDie() // invoke when enemy die
    {
        stateMachine.SwitchState(enemyDieState);
    }
    private void SpawnNewEnemy() 
    {
        stateMachine.SwitchState(newSpawnEnemyState);
    }
}
