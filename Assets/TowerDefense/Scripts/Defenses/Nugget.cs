using UnityEngine;

public class Nugget : MonoBehaviour
{
    private DefensesData data;
    public int damage = 10;
    public int _health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            //Deja de moverse
            //other.gameObject.GetComponent<EnemyMovement>().StopMoving();
            //Le hace daño al enemigo
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            other.gameObject.GetComponent<Enemy>().MakeDamage(_health);
        }
        
    }

    public void OnTriggerExit(Collider other)
    {

    }

}
