using Unity.VisualScripting;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance;
    [SerializeField]private float gold;

    public float Gold
    {
        get
        {
            return gold;
        }
    }
    private UIManager manager;
    public void Initialize()
    {
        instance = this;
        manager = FindObjectOfType<UIManager>();
    }

    public bool RemoveGold(float value)
    {
        if(gold >= value)
        {
            gold-=value;

            manager.UpdateGold(gold);
            return true;
        }
        else
        {
            return false;
        }
    }
    public void AddGold(float value)
    {
        gold += value;
        manager.UpdateGold(gold);
        
    }
}
