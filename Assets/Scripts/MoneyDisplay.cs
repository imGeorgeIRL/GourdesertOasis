using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    public TextMeshProUGUI moneyText; // Reference to the UI Text component


    private void Update()
    {
        // Update the displayed money value based on the player's money variable

        if (MoneyManager.instance.playerMoney % 1.0f == 0.0f)
        {
            moneyText.text = MoneyManager.instance.playerMoney.ToString();
        }
        else
        {
            moneyText.text = MoneyManager.instance.playerMoney.ToString("F2");
        }
    }
}
