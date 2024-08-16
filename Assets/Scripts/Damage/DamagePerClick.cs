using System;
using UnityEngine;

public class DamagePerClick : MonoBehaviour
{
    public event Action ActiveDoubleDamage;
    public event Action NoActiveDoubleDamage;
    public static event Action<Vector3> OnClickHit;

    [SerializeField] private float DamageMultiplierThreshold = 8f;

    private UpgradeManager _upgradeManager;
    private GameStateManager _gameStateManager;
    private UIManager _uiManager;

    private float damageMultiplier = 1;
    const float defaultClicksPerAction = 1;
    public void Initialize(UpgradeManager upgradeManager, GameStateManager gameStateManager, UIManager uIManager)
    {
        _upgradeManager = upgradeManager;
        _gameStateManager = gameStateManager;
        _uiManager = uIManager;
        _uiManager.UpdateDamagePerClick(defaultClicksPerAction);
    }
    private void OnEnable()
    {
        InputManager.OnClick += Click;
    }
    private void OnDisable()
    {
        InputManager.OnClick -= Click; 
    }
    public void OnClicksAccumulated(float accumulatedClicks)
    {
        if (accumulatedClicks >= DamageMultiplierThreshold)
        {
            ActiveDoubleDamage?.Invoke();
            damageMultiplier = 2;
        }
        else
        {
            NoActiveDoubleDamage?.Invoke();
            damageMultiplier = 1;
        }
    }
    private void Click(Vector3 pos)
    {
        if (!_gameStateManager.isGame) return;
        _uiManager.UpdateDamagePerClick(GetDamage());
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                float damage = GetDamage();
                //Debug.LogError(damage);
                OnClickHit?.Invoke(hit.point);

                damageable.TakeDamage(damage);

                GoldManager.instance.AddGold(damage);

            }
        }
    }    

    public float GetDamage()
    {
        return (_upgradeManager.GetAllDamagePerClick() + defaultClicksPerAction) * damageMultiplier;
    }
}
