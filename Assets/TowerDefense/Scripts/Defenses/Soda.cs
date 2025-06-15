using System.Xml.Serialization;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Soda : MonoBehaviour
{
    private float counting;
    public float sodaTime= 30;
    public float liquidTime= 30;

    public GameObject sodaModel;
    public GameObject liquidModel;
    public int damage = 1;

    private EnemyManager enemyManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
        StartCoroutine(Timing());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            enemy.ActiveFreeze();
        }
    }

    private IEnumerator Timing()
    {
        yield return new WaitForSeconds(sodaTime);
        sodaModel.SetActive(false);
        liquidModel.SetActive(true);

        yield return new WaitForSeconds(liquidTime);
        this.gameObject.SetActive(false);
    }
}
