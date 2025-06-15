using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Disparo")]
    public GameObject bulletPrefab;
    public Transform firePoint; 
    public float shootInterval = 1.5f;
    private float shootTimer;

    [Header("Detección")]
    public float attackRange = 3f;

    [Header("Audio")]
    public AudioSource shootSound;

    private GameObject currentTarget;

    void Start()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward);
    }

    void Update()
    {
        currentTarget = FindClosestEnemy();

        if (currentTarget != null)
        {
            RotateTowards(currentTarget);

            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                Shoot();
                shootTimer = 0f;
            }
        }
    }

    void Shoot()
    {
        if (shootSound != null)
            shootSound.Play();

        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float shortestDistance = attackRange;

        foreach (GameObject enemy in enemies)
        {
            if (!enemy.activeInHierarchy) continue;

            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = enemy;
            }
        }

        return closest;
    }

    void RotateTowards(GameObject target)
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Vector3 newRotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, newRotation.y, 0f);
        }
    }
}
