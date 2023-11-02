using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowInteractions : MonoBehaviour
{

    public GameObject sorryPanel;


    private void Update()
    {

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
                    sorryPanel.SetActive(true);
                    MoneyManager.uiActive = true;
                }

            }

        }
    }

    public void uiInactive()
    {
        MoneyManager.uiActive = false;
        sorryPanel.SetActive(false);
    }

}
