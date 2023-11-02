using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandInteraction : MonoBehaviour
{

    public GameObject forSaleSign;
    public GameObject plantStates;

    private PlantGrowing plantGrowing;

    public GameObject purchasePanel;
    private GameObject thisGameObject;



    [SerializeField] private bool beenPurchased;

    private void Start()
    {
        // Make sure the UI panel is initially inactive

        thisGameObject = gameObject;

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
        else if (beenPurchased)
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

                if (hitCollider != null && hitCollider.gameObject == thisGameObject)
                {
                    if (beenPurchased)
                    {
                        plantGrowing.ResetPlant();
                    }
                    else
                    {
                        PurchasePlant(thisGameObject);
                    }
                }

            }
          

        }
    }

    private void HandleHover()
    {
        if (!beenPurchased)
        {
            // Convert the mouse position to world coordinates in 2D space
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse position is over the sprite
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            if (hitCollider != null && hitCollider.gameObject == gameObject)
            {
                forSaleSign.SetActive(true);
            }
            else
            {
                forSaleSign.SetActive(false);
            }
        }
    }


    public void DoPurchase()
    {
        PurchasePlant(thisGameObject);
    }


    public void PurchasePlant(GameObject plant)
    {
        if (MoneyManager.instance.playerMoney >= 10000)
        {
            MoneyManager.instance.playerMoney -= 10000;
            beenPurchased = true;
            plantGrowing.enabled = true;
            plantStates.SetActive(true);
            forSaleSign.SetActive(false);
            purchasePanel.SetActive(false);
            MoneyManager.uiActive = false;
        }
    }


}

