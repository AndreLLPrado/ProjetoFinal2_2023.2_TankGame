using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuDisplayController : MonoBehaviour
{
    [SerializeField]
    private Text cashText;

    [SerializeField]
    private int[] skillLevels;
    [SerializeField]
    private int[] skillCosts;

    // Speed
    [Header("Speed")]
    [SerializeField]
    private Text SpeedLevel;
    [SerializeField]
    private Text SpeedCost;
    [SerializeField]
    private Image SpeedBar;
    [SerializeField]
    private GameObject SpeedMaxText;
    [SerializeField]
    private Button UpgradeSpeedButton;

    // HP
    [Header("HP")]
    [SerializeField]
    private Text HPLevel;
    [SerializeField]
    private Text HPCost;
    [SerializeField]
    private Image HPBar;
    [SerializeField]
    private GameObject HPMaxText;
    [SerializeField]
    private Button UpgradeHPButton;

    //Fire Rate
    [Header("Fire Rate")]
    [SerializeField]
    private Text FRLevel;
    [SerializeField]
    private Text FRCost;
    [SerializeField]
    private Image FRBar;
    [SerializeField]
    private GameObject FRMaxText;
    [SerializeField]
    private Button UpgradeFRButton;

    //Damage
    [Header("Damage")]
    [SerializeField]
    private Text DamageLevel;
    [SerializeField]
    private Text DamageCost;
    [SerializeField]
    private Image DamageBar;
    [SerializeField]
    private GameObject DamageMaxText;
    [SerializeField]
    private Button UpgradeDamageButton;
    private void Start()
    {
        skillLevels = new int[4];
        skillCosts = new int[4];
    }
    private void Update()
    {
        cashText.text = "cash: " + GameObject.Find("MenusController").GetComponent<UpgradeMenuController>().getCash().ToString();
        loadSkillsValues();
        UpgradeSpeedDisplay();
        UpgradeHPDisplay();
        UpgradeFRDisplay();
        UpgradeDamageDisplay();
    }

    private void UpgradeSpeedDisplay()
    {
        SpeedLevel.text = "Lv: " + skillLevels[0].ToString();
        SpeedCost.text = "$" + skillCosts[0].ToString();

        float progress = (float)skillLevels[0] / 5f;
        SpeedBar.fillAmount = progress;

        if (skillLevels[0] >= 5)
        {
            SpeedMaxText.SetActive(true);
        }
        else
        {
            SpeedMaxText.SetActive(false);
        }

        if (skillLevels[0] > 4 || skillCosts[0] > GameObject.Find("MenusController").GetComponent<UpgradeMenuController>().getCash())
        {
            UpgradeSpeedButton.interactable = false;
        }
        else
        {
            UpgradeSpeedButton.interactable = true;
        }
    }

    private void UpgradeHPDisplay()
    {
        HPLevel.text = "Lv: " + skillLevels[1].ToString();
        HPCost.text = "$" + skillCosts[1].ToString();

        float progress = (float)skillLevels[1] / 5f;
        HPBar.fillAmount = progress;

        if (skillLevels[1] >= 5)
        {
            HPMaxText.SetActive(true);
        }
        else
        {
            HPMaxText.SetActive(false);
        }

        if (skillLevels[1] > 4 || skillCosts[1] > GameObject.Find("MenusController").GetComponent<UpgradeMenuController>().getCash())
        {
            UpgradeHPButton.interactable = false;
        }
        else
        {
            UpgradeHPButton.interactable = true;
        }
    }

    private void UpgradeFRDisplay()
    {
        FRLevel.text = "Lv: " + skillLevels[2].ToString();
        FRCost.text = "$" + skillCosts[2].ToString();

        float progress = (float)skillLevels[2] / 5f;
        FRBar.fillAmount = progress;

        if (skillLevels[2] >= 5)
        {
            FRMaxText.SetActive(true);
        }
        else
        {
            FRMaxText.SetActive(false);
        }

        if (skillLevels[2] > 4 || skillCosts[2] > GameObject.Find("MenusController").GetComponent<UpgradeMenuController>().getCash())
        {
            UpgradeFRButton.interactable = false;
        }
        else
        {
            UpgradeFRButton.interactable = true;
        }
    }

    private void UpgradeDamageDisplay()
    {
        DamageLevel.text = "Lv: " + skillLevels[3].ToString();
        DamageCost.text = "$" + skillCosts[3].ToString();

        float progress = (float)skillLevels[3] / 5f;
        DamageBar.fillAmount = progress;

        if (skillLevels[3] >= 5)
        {
            DamageMaxText.SetActive(true);
        }
        else
        {
            DamageMaxText.SetActive(false);
        }

        if (skillLevels[3] > 4 || skillCosts[3] > GameObject.Find("MenusController").GetComponent<UpgradeMenuController>().getCash())
        {
            UpgradeDamageButton.interactable = false;
        }
        else
        {
            UpgradeDamageButton.interactable = true;
        }
    }

    void loadSkillsValues()
    {
        skillLevels = GameObject.Find("MenusController").GetComponent<UpgradeMenuController>().getSkillLevels();
        skillCosts = GameObject.Find("MenusController").GetComponent<UpgradeMenuController>().getSkillCosts();
    }
}
