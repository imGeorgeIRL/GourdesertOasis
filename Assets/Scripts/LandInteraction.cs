using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandInteraction : MonoBehaviour
{


    //private bool panelActive = false; // To track if the UI panel is active

    private PlantGrowing plantGrowing;

    private void Start()
    {
        // Make sure the UI panel is initially inactive


        plantGrowing = GetComponent<PlantGrowing>();
    }

    private void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {

            // Convert the mouse position to world coordinates in 2D space
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse click is over the sprite
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            if (hitCollider != null && hitCollider.gameObject == gameObject)
            {
                plantGrowing.ResetPlant();
            }
        }
    }
}

