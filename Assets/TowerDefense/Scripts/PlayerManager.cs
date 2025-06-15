using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public float totalMoney = 35;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI friesCostText;
    public TextMeshProUGUI nuggetCostText;
    public TextMeshProUGUI sodaCostText;
    public TextMeshProUGUI sauceCostText;

    public GameObject OpenFridgeFries;
    public GameObject OpenFridgeSoda;
    public GameObject OpenFridgeNugget;

    public GameObject CloseFridgeFries;
    public GameObject CloseFridgeSoda;
    public GameObject CloseFridgeNugget;


    public float friesCost = 10;
    public float sodaCost = 20;
    public float nuggetCost = 30;
    public float sauceCost = 50;

    public AudioSource moneySound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        UpdateMoneyText();
        friesCostText.text = ("$" + friesCost);
        nuggetCostText.text = ("$" + nuggetCost);
        sodaCostText.text = ("$" + sodaCost);
        sauceCostText.text = ("$" + sauceCost);

        CheckImageFridge();
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
            CheckImageFridge();
            return true;
        }
        return false;
    }

    void UpdateMoneyText()
    {
        if (moneyText != null)
            moneyText.text = "$" + totalMoney.ToString("0");
    }

    public void AddMoney(int amount)
    {
        totalMoney += amount;
        UpdateMoneyText();
        CheckImageFridge();
        moneySound.Play();
    }

    public void CheckImageFridge()
    {
        if (totalMoney >= friesCost)
        {
            OpenFridgeFries.gameObject.SetActive(true);
            CloseFridgeFries.gameObject.SetActive(false);
        }
        else
        {
            OpenFridgeFries.gameObject.SetActive(false);
            CloseFridgeFries.gameObject.SetActive(true);
        }
        if (totalMoney > sodaCost)
        {
            OpenFridgeSoda.gameObject.SetActive(true);
            CloseFridgeSoda.gameObject.SetActive(false);
        }
        else
        {
            OpenFridgeSoda.gameObject.SetActive(false);
            CloseFridgeSoda.gameObject.SetActive(true);
        }
        if (totalMoney >= nuggetCost)
        {
            OpenFridgeNugget.gameObject.SetActive(true);
            CloseFridgeNugget.gameObject.SetActive(false);
        }
        else
        {
            OpenFridgeNugget.gameObject.SetActive(false);
            CloseFridgeNugget.gameObject.SetActive(true);
        }

    }

}