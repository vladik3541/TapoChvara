using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class AbilittiCell : MonoBehaviour
{
    [SerializeField] private Ability ability;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textPrice;
    [SerializeField] private TextMeshProUGUI textCountUpgrade;
    [SerializeField] private TextMeshProUGUI textDescription;
    [SerializeField] private Color32 lockButton, unlockButton;

    private UpgradeManager upgradeManager;
    private Image currentButtonImage;
    private float timeDelayCheck = 0.5f;
    private void Start()
    {
        upgradeManager = FindObjectOfType<UpgradeManager>();
        currentButtonImage = GetComponent<Image>();
        textName.text = ability.Name;
        
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
                currentButtonImage.color = lockButton;
            }
            else
            {
                currentButtonImage.color = unlockButton;
            }
            yield return new WaitForSeconds(timeDelayCheck);
        }
    }
}
