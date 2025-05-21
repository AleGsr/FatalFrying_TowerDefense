using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public float totalMoney = 100;
    public TextMeshProUGUI moneyText;

    public float friesCost = 10;
    public float sodaCost = 20;
    public float nuggetCost = 30;
    public float sauceCost = 50;

    private void Start()
    {
        UpdateMoneyText();
    }

    public bool FriesCost() { return TryBuy(friesCost); }
    public bool SodaCost() { return TryBuy(sodaCost); }
    public bool NuggetCost() { return TryBuy(nuggetCost); }
    public bool SaucesCost() { return TryBuy(sauceCost); }

    bool TryBuy(float cost)
    {
        if (totalMoney >= cost)
        {
            totalMoney -= cost;
            UpdateMoneyText();
            return true;
        }
        return false;
    }

    void UpdateMoneyText()
    {
        if (moneyText != null)
            moneyText.text = "$" + totalMoney.ToString("0");
    }
}
