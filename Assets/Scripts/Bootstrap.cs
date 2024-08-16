using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private SaveManager saveManager;
    [SerializeField] private GameStateManager gameStateManager;
    public UIAnimation uIAnimation;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private DamagePerClick damagePerClick;
    [SerializeField] private DamagePerSeconds damagePerSeconds;
    [SerializeField] private AccumulatedDamage accumulatedDamage;
    [SerializeField] private GoldManager goldManager;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private EffectClickManager effectClickManager;
    

    private void Awake()
    {
        saveManager.Initialize();
        uiManager.Initialize(uIAnimation, accumulatedDamage, damagePerClick);
        gameStateManager.Initialize();
        damagePerClick.Initialize(upgradeManager, gameStateManager, uiManager);
        damagePerSeconds.Initialize();
        accumulatedDamage.Initialize();
        goldManager.Initialize();
        effectClickManager.Initialize(damagePerClick);
        
    }

}
