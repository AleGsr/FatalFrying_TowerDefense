using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fries : MonoBehaviour
{
    private DefensesData data;



    [Header("Disapear")]
    public float totalTime = 30;
    public float countTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //healthFries = data.healthDefense;
        countTime = totalTime;

    }

    // Update is called once per frame
    void Update()
    {
        //fireCooldown -= Time.deltaTime;

        //Shoot();
        Timing();
        Disapear();
        
    }



    void Disapear()
    {
        if(countTime <= 0)
        {
            countTime = 0;
            this.gameObject.SetActive(false);
        }
    }

    void Timing()
    {
        countTime -= Time.deltaTime;
    }


}
