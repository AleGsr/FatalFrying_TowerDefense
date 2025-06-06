using UnityEngine;

public class Money : MonoBehaviour
{
    public int minMoney = 5;
    public int maxMoney = 30;

    void OnMouseDown()
    {
        int moneyAmount = Random.Range(minMoney, maxMoney);
        PlayerManager.Instance.AddMoney(moneyAmount); // Sumar dinero al jugador

        Destroy(gameObject); // Eliminar el objeto de dinero al recogerlo
    }
}
