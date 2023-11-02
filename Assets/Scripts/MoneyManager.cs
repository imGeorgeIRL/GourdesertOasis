using UnityEditor;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance; // Singleton instance

    public float playerMoney = 0; // The player's total money

    public static bool uiActive = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            playerMoney += 5000;
        }
    }
}
