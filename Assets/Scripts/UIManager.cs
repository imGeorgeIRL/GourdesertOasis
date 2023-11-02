using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{

    public GameObject[] panels; // Array to hold your UI panels
    private int currentPanelIndex = -1; // Initialize with -1 to indicate no active panel initially

    // Function to activate a specific panel and deactivate the currently active one
    public void ActivatePanel(int panelIndex)
    {
        if (panelIndex >= 0 && panelIndex < panels.Length)
        {
            // Deactivate the current panel if there is one
            if (currentPanelIndex >= 0 && currentPanelIndex < panels.Length)
            {
                panels[currentPanelIndex].SetActive(false);
            }

            // Activate the new panel
            panels[panelIndex].SetActive(true);
            currentPanelIndex = panelIndex; // Update the current panel index
        }
        else
        {
            Debug.LogWarning("Invalid panel index.");
        }
    }
}
