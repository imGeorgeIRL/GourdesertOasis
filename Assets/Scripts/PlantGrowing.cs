using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantGrowing : MonoBehaviour
{
    public GameObject[] plantStates;
    public GameObject[] fences;

    [SerializeField] private string plantName;
    public GameObject uiPanel; // Reference to the UI panel
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI upgradeCostText;
    //money multiplier...
    //growth time multiplier

    private float baseValue;
    private float baseGrowthSpeed;

    private bool uiPanelActive = false;

    private int currentState = 0; // 0 represents the seed state
    public int upgradeLevel = 0;

    private float timeSinceLastGrowth = 0f;
    private float timeToNextGrowth = 5f; // Time in seconds for each growth stage

    private float valueModifier = 5f; // Added growthSpeed variable
    private float growthSpeed;

    private float upgradeCost; // Cost of the next upgrade
    public float initialUpgradeCost = 10f; // Initial cost for the first upgrade
    public float upgradeCostIncrease = 5f; // Cost increase per upgrade level
    void Start()
    {
        SetPlantState(currentState);

        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }

        switch (plantName)
        {
            case "Pumpkin":
                baseValue = 1000;
                baseGrowthSpeed = 5f;
                break;
            case "Butternut":
                baseValue = 1.25f;
                baseGrowthSpeed = 5f;
                break;
            case "Zucchini":
                baseValue = 1.75f;
                baseGrowthSpeed = 5f;
                break;
            case "Watermelon":
                baseValue = 2.5f;
                baseGrowthSpeed = 5f;
                break;
            default:
                Debug.LogWarning("There is no case for this Plant name");
                break;
        }

        upgradeCost = initialUpgradeCost;

        ApplyUpgrades();

        
    }


    void Update()
    {
        // Check if it's time to grow to the next state
        timeSinceLastGrowth += Time.deltaTime;

        if (timeSinceLastGrowth >= timeToNextGrowth && currentState < plantStates.Length - 1)
        {
            currentState++;
            SetPlantState(currentState);
            timeSinceLastGrowth = 0f;
        }

        if (uiPanelActive && Input.GetKeyDown(KeyCode.Escape))
        {
            uiPanelActive = false;
            uiPanel.SetActive(false);
        }

        levelText.text = "Level: " + upgradeLevel.ToString();
        upgradeCostText.text = "$" + upgradeCost.ToString();

        switch (upgradeLevel)
        {
            case 0:
                foreach (GameObject fence in fences)
                {
                    fence.SetActive(false);
                }
                break;
            case 25:
                fences[0].SetActive(true);
                break;
            case 50:
                fences[1].SetActive(true);
                fences[0].SetActive(false);
                break;
            case 75:
                fences[1].SetActive(false);
                fences[2].SetActive(true);
                break;
            case 100:
                fences[2].SetActive(false);
                fences[3].SetActive(true);
                break;
                default: break;
        }



    }

    private void SetPlantState(int state)
    {
        for (int i = 0; i < plantStates.Length; i++)
        {
            plantStates[i].SetActive(i == state);
        }
    }

    public void UpgradePlant()
    {
        if (upgradeLevel < 100) 
        {
            // Check if the player has enough money to purchase the upgrade
            if (MoneyManager.instance.playerMoney >= upgradeCost)
            {
                // Deduct the upgrade cost from the player's money
                MoneyManager.instance.playerMoney -= upgradeCost;

                upgradeLevel++;
                ApplyUpgrades();

                // Check if the current level is a multiple of 25
                if ((upgradeLevel + 1) % 25 == 0)
                {
                    // Increase the upgrade cost by a factor of 5
                    upgradeCost *= 5;
                }
                else
                {
                    // Increase the upgrade cost using the regular upgrade cost increase
                    upgradeCost += upgradeCostIncrease;
                }
            }
        }
    }

    // Function to apply upgrades based on the upgrade level
    private void ApplyUpgrades()
    {
        baseValue = baseValue + upgradeLevel * 0.25f;
        //growthSpeed = baseGrowthSpeed - (upgradeLevel * 0.005f);
        //timeToNextGrowth = 5.0f / growthSpeed; // Adjust time based on growth speed
    }

    // Function to reset the plant growth cycle
    public void ResetPlant()
    {
        if (currentState == plantStates.Length - 1)
        {
            // Plant is fully grown, reset to seed state
            currentState = 0;
            SetPlantState(currentState);

            timeSinceLastGrowth = 0f;

            // Calculate and add money to the player's total
            float moneyEarned = baseValue * valueModifier;
            MoneyManager.instance.playerMoney += moneyEarned;
        }
        else
        {
            // Plant is not fully grown, show the UI panel
            if (!uiPanelActive)
            {
                uiPanel.SetActive(true);
                uiPanelActive = true;
            }
        }
    }

    public void CloseUI()
    {
        uiPanel.SetActive(false);
        uiPanelActive = false;
        Debug.Log("X button pressed");
    }

}
