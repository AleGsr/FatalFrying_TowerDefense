using UnityEngine;
using System.Collections;
public class Nugget : MonoBehaviour
{
    public int damage = 10;
    public int _health;

    [Header("Disapear")]
    public float totalTime = 30;
    public float countTime;

    public float damageTiming = 2;

    public GameObject model;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Timing());
    }

    // Update is called once per frame
    void Update()
    {
        if (_health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            enemy.MakeDamage(_health);
        }

        
    }

    private IEnumerator Timing()
    {
        yield return new WaitForSeconds(totalTime);
        this.gameObject.SetActive(false);
    }


}



