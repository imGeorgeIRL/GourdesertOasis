using UnityEngine;
using UnityEngine.UI;

public class LevelSlider : MonoBehaviour
{
    public Slider slider; // Reference to the UI Slider
    public PlantGrowing plantController; // Reference to the PlantController script

    private int currentBigLevel = 0;
    private int bigLevelIncrement = 25; // The increment for big levels

    private void Start()
    {
        if (slider != null)
        {
            slider.minValue = 0;
            slider.maxValue = 25; // Set the maximum level based on the PlantController
        }
    }

    private void Update()
    {
        if (slider != null)
        {
            slider.value = plantController.upgradeLevel; // Continuously update the slider value based on upgradeLevel
        }
    }
}
