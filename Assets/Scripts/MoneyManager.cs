using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance; // Singleton instance

    public float playerMoney = 0; // The player's total money


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
