using UnityEngine;

public class Money : MonoBehaviour
{
    public int minMoney = 5;
    public int maxMoney = 20;


    void OnMouseDown()
    {
        int moneyAmount = Random.Range(minMoney, maxMoney);
        PlayerManager.Instance.AddMoney(moneyAmount); 
        this.gameObject.SetActive(false); 
    }
}
