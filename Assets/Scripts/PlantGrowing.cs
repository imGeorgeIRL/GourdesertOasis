using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantGrowing : MonoBehaviour
{
    [Header("Arrays")]
    public GameObject[] plantStates;
    public GameObject wireFence, woodenFence, stoneFence, hedgeFence;
    public GameObject wireFence2, woodenFence2, stoneFence2, hedgeFence2;


    [Header("Plant Name")]
    [SerializeField] private string plantName;

    [Header("UI")]
    public GameObject uiPanel; // Reference to the UI panel
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI upgradeCostText;
    public TextMeshProUGUI priceValueText;
    private LandInteraction landInteraction;

    private float cropValue;

    public ParticleSystem particles;
    private bool particlePlayed;

    
    private float baseValue;
    private float baseGrowthSpeed;

    public bool uiPanelActive = false;

    private int currentState = 0; // 0 represents the seed state

    private int upgradeLevel = 0;

    private float timeSinceLastGrowth = 0f;
    private float timeToNextGrowth = 5f; // Time in seconds for each growth stage

    private float valueModifier;

    public float upgradeCost; // Cost of the next upgrade


    private float initialUpgradeCost = 10f; // Initial cost for the first upgrade
    private float upgradeCostIncrease = 5f; // Cost increase per upgrade level

    private float minGrowthTime;
    private float maxGrowthTime;

    void Start()
    {
        landInteraction = GetComponent<LandInteraction>();
        SetPlantState(currentState);

        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }

        switch (plantName)
        {
            case "Pumpkin":
                baseValue = 10;
                timeToNextGrowth = 5f;
                minGrowthTime = 5f;
                maxGrowthTime = 10f;

                valueModifier = 1f;
                break;
            case "Butternut":
                baseValue = 15f;
                timeToNextGrowth = 5f;
                minGrowthTime = 10f;
                maxGrowthTime = 15f;

                valueModifier = 1f;
                break;
            case "Zucchini":
                baseValue = 25f;
                timeToNextGrowth = 5f;
                minGrowthTime = 15f;
                maxGrowthTime = 20f;

                valueModifier = 1f;
                break;
            case "Watermelon":
                baseValue = 40f;
                timeToNextGrowth = 5f;
                minGrowthTime = 20f;
                maxGrowthTime = 25f;

                valueModifier = 1f;
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
        priceValueText.text = "$" + baseValue * valueModifier;

        // Check if it's time to grow to the next state
        timeSinceLastGrowth += Time.deltaTime;

        if (timeSinceLastGrowth >= timeToNextGrowth && currentState < plantStates.Length - 1)
        {
            currentState++;
            SetPlantState(currentState);
            timeSinceLastGrowth = 0f;
            timeToNextGrowth = Random.Range(minGrowthTime, maxGrowthTime);
            if (currentState == plantStates.Length -1 && !particlePlayed)
            {
                particles.Play();
                particlePlayed = true;
            }
        }

        if (MoneyManager.uiActive)
        {
            landInteraction.enabled = false;
        }
        else if (!MoneyManager.uiActive)
        {
            landInteraction.enabled = true;
        }

        if (MoneyManager.uiActive && Input.GetKeyDown(KeyCode.Escape)) // -------------------- UI DEACTIVATION
        {
            MoneyManager.uiActive = false;

            uiPanel.SetActive(false);
        }

        if (upgradeCost % 1.0f == 0.0f)
        {
            upgradeCostText.text = "$" + upgradeCost.ToString();            
        }
        else
        {
            upgradeCostText.text = "$" + upgradeCost.ToString("F2");
        }

        levelText.text = "Level: " + upgradeLevel.ToString();


        switch (upgradeLevel)
        {
            case 0:
                
                break;
            case 25:
                wireFence.SetActive(true);
                wireFence2.SetActive(true);
                break;
            case 50:
                woodenFence.SetActive(true);
                woodenFence2.SetActive(true);
                wireFence.SetActive(false);
                wireFence2.SetActive(false);
                break;
            case 75:
                woodenFence.SetActive(false);
                woodenFence2.SetActive(false);
                stoneFence.SetActive(true);
                stoneFence2.SetActive(true);
                break;
            case 100:
                stoneFence.SetActive(false);
                stoneFence2.SetActive(false);
                hedgeFence.SetActive(true);
                hedgeFence2.SetActive(true);
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
                Debug.Log("Upgrade level is now " + upgradeLevel);

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

                upgradeCostIncrease *= 1.1f;
                valueModifier *= 1.05f;
                Debug.Log("Value Modifier is now: " + valueModifier);
                Debug.Log("Upgrade Cost Increase is now: " + upgradeCostIncrease);
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

            particlePlayed = false;

            // Calculate and add money to the player's total
            float moneyEarned = baseValue * valueModifier;
            MoneyManager.instance.playerMoney += moneyEarned;
            
        }
        else
        {
            
            if (!MoneyManager.uiActive) //----------------------------- UI ACTIVATION HERE
            {
                uiPanel.SetActive(true);
                MoneyManager.uiActive = true;
            }
        }
    }

    public void CloseUI()
    {
        uiPanel.SetActive(false);
        MoneyManager.uiActive = false;
        Debug.Log("X button pressed");
    }

}
