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
}
