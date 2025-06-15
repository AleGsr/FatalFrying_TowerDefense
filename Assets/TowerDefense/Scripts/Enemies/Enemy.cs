using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int currentHealth;
    public int damage;
    public float makedamageTiming = 4;
    public float getdamageTiming = 2;

    private EnemyData data;
    private List<Vector3> path;
    private int currentWaypoint = 0;

    [SerializeField] private GameObject moneyPrefab;
    public AudioSource enemyDamage;
    public AudioSource enemyDie;
    public AudioSource makeDamage;
    public ParticleSystem freeze;

    private bool isDead = false;

    public float speed = 2f;
    private float originalSpeed;
    private bool isSlowed = false;
    private bool isStopped = false;

    private Coroutine damageCoroutine;
    private Coroutine getDamageCoroutine;

    public int CurrentHealth => currentHealth;


    public void Initialize(EnemyData enemyData, List<Vector3> waypoints)
    {
        data = enemyData;
        path = new List<Vector3>(waypoints);
        currentWaypoint = 0;
        currentHealth = data.health;
        isDead = false; 
        enabled = true;

        originalSpeed = data.speed;
    }

    void Start()
    {
        originalSpeed = speed;
    }
    void Update()
    {
        if (isDead ) return;

        if (path.Count > 0 && currentWaypoint < path.Count)
        {
            Vector3 targetPosition = new Vector3(path[currentWaypoint].x, transform.position.y, path[currentWaypoint].z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentWaypoint++;
            }
        }
        else if (currentWaypoint >= path.Count)
        {
            enabled = false;
        }

        if (currentHealth <= 0 && !isDead)
        {
            StartCoroutine(DieAfterSound());
            isDead = true; 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag("Bullet"))
        {
            enemyDamage.Play();
            currentHealth -= 5;
            if (currentHealth <= 0 && !isDead)
            {
                StartCoroutine(DieAfterSound());
                isDead = true;
            }
            Destroy(other.gameObject); 
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
            StartCoroutine(GettingDamage(damage));
    }

    public void ActiveFreeze() => freeze.Play();
    public void DesactiveFreeze() => freeze.Stop();

    private void DropMoney()
    {
        if (moneyPrefab != null)
        {
            Instantiate(moneyPrefab, transform.position + Vector3.up * 1f, Quaternion.identity);
        }
    }

    private IEnumerator DieAfterSound()
    {
        enemyDie.Play();
        yield return new WaitForSeconds(enemyDie.clip.length);
        Die();
    }

    private void Die()
    {
        DropMoney();
        EnemyManager enemyManager = FindObjectOfType<EnemyManager>();
        enemyManager.RecycleEnemy(this.gameObject);
    }

    public void MakeDamage(int defenseHealth)
    {
        if (!isDead)
            StartCoroutine(MakingDamage(defenseHealth));
    }

    private IEnumerator MakingDamage(int defenseHealth)
    {
        yield return new WaitForSeconds(makedamageTiming);
        makeDamage.Play();
        defenseHealth -= damage;
    }

    private IEnumerator GettingDamage(int defenseDamage)
    {
        yield return new WaitForSeconds(getdamageTiming);
        enemyDamage.Play();
        currentHealth -= defenseDamage;
    }

    public void StoppingDamage()
    {
        StopAllCoroutines();
    }


    public void SlowDown(float factor)
    {
        speed = originalSpeed * factor;
    }

    public void RestoreSpeed()
    {
        speed = originalSpeed;
    }

    public void StopMovement()
    {
        isStopped = true;
    }

    public void ResumeMovement()
    {
        isStopped = false;
    }




}
