using System.Xml.Serialization;
using UnityEngine;

public class Soda : MonoBehaviour
{
    float totalCount = 30;
    public float counting;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        counting = totalCount;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        Disapear();
    }

    void SlowEnemy()
    {
        //Cuando lo pisen su velocidad bajará temporalmente
    }

    void Damage()
    {
        //Cuando lo pisen se les bajara la vida
    }

    void Timer()
    {
        counting -= Time.deltaTime;
    }

    void Disapear()
    {
        if(counting <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
