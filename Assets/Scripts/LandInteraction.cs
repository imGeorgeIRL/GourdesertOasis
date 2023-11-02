using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandInteraction : MonoBehaviour
{

    public GameObject forSaleSign;
    public GameObject plantStates;
    //private bool panelActive = false; // To track if the UI panel is active

    private PlantGrowing plantGrowing;

    public GameObject purchasePanel;

    public bool beenPurchased;

    private void Start()
    {
        // Make sure the UI panel is initially inactive


        plantGrowing = GetComponent<PlantGrowing>();
    }

    private void Update()
    {
        if (!beenPurchased)
        {
            plantGrowing.enabled = false;
            plantStates.SetActive(false);
            forSaleSign.SetActive(true);
        }
        else
        {
            plantGrowing.enabled = true;
            plantStates.SetActive(true);
            forSaleSign.SetActive(false);
        }

        // Check for mouse click
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            
                if (!MoneyManager.uiActive)
                {
                    // Convert the mouse position to world coordinates in 2D space
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    // Check if the mouse click is over the sprite
                    Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

                    if (hitCollider != null && hitCollider.gameObject == gameObject)
                    {
                        if (beenPurchased) 
                        { 
                            plantGrowing.ResetPlant();
                        }
                        else
                        {
                            purchasePanel.SetActive(true);
                            MoneyManager.uiActive = true;
                        }
                }

                }
            
                //if (!MoneyManager.uiActive)
                //{
                //    purchasePanel.SetActive(true);
                //    MoneyManager.uiActive = true;
                //}
            
        }
    }

    public void Purchasing()
    {
        if (MoneyManager.instance.playerMoney >= 10000)
        {
            MoneyManager.instance.playerMoney -= 10000;
            beenPurchased = true;
        }
    }

    public void uiInactive()
    {
        MoneyManager.uiActive = false;
    }

}

