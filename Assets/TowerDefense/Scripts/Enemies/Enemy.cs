using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public int damage;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

    }

    // Update is called once per frame
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void MakeDamage(int defenseHealth)
    {
        defenseHealth =- damage;
    }


}
