using UnityEngine;
using UnityEngine.UI;

public class LevelSlider : MonoBehaviour
{
    private Slider slider; // Reference to the UI Slider

    public PlantGrowing plantGrowing;

    

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 0;
    }

    private void Update()
    {
        if (slider.value == 25)
        {
            slider.value = 0;
        }
    }

    public void IncreaseSlider()
    {
        if (MoneyManager.instance.playerMoney > plantGrowing.upgradeCost)
        {
            slider.value++;
        }
    }

}
