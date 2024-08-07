using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class AbilittiCell : MonoBehaviour
{
    [SerializeField] private Ability ability;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI textPrice;
    [SerializeField] private TextMeshProUGUI textCountUpgrade;
    [SerializeField] private TextMeshProUGUI textDescription;

    private UpgradeManager upgradeManager;
    private Image currentButtonImage;
    private float timeDelayCheck = 0.5f;
    private void Start()
    {
        upgradeManager = FindObjectOfType<UpgradeManager>();
        currentButtonImage = GetComponent<Image>();
        textName.text = ability.Name;
        icon.sprite = ability.Sprite;
        textPrice.text = ability.Price.ToString("N0") + "$";
        textCountUpgrade.text = "0";
        textDescription.text = ability.Description;
        StartCoroutine(CheckGold());
    }

    public void UpdateTextUpgrade()
    {
        textCountUpgrade.text = upgradeManager.GetUpgradeLevel(ability).ToString();
    }

    private IEnumerator CheckGold()
    {
        while (true)
        {
            if (GoldManager.instance.Gold < ability.Price)
            {
                currentButtonImage.color = Color.gray;
            }
            else
            {
                currentButtonImage.color = Color.white;
            }
            yield return new WaitForSeconds(timeDelayCheck);
        }
    }
}
