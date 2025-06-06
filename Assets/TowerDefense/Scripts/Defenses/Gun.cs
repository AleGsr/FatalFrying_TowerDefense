using UnityEngine;
using System.Collections;
public class Gun : Defense
{
    public GameObject bulletPrefab;
    public float shootInterval = 1.5f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= shootInterval)
        {
            Shoot();
            timer = 0f;
        }

    }

    void Shoot()
    {
        Instantiate(bulletPrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);
    }
}
