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
        moneyText.text = MoneyManager.instance.playerMoney.ToString("0");
    }
}
