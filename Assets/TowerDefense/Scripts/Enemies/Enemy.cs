using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    private int currentHealth;
    public int damage;


    private EnemyData data;
    private List<Vector3> path;
    private int currentWaypoint = 0;

    [SerializeField] private GameObject moneyPrefab; // Prefab del dinero

    public void Initialize(EnemyData enemyData, List<Vector3> waypoints)
    {
        data = enemyData;
        path = waypoints;
        currentHealth = data.health;
    }

    void Update()
    {
        if (path.Count > 0 && currentWaypoint < path.Count)
        {
            Vector3 targetPosition = new Vector3(path[currentWaypoint].x, transform.position.y, path[currentWaypoint].z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, data.speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentWaypoint++;
            }
        }
        else if (currentWaypoint >= path.Count) // Si el enemigo termina su recorrido, dejarlo en su posición final
        {
            // Dejar de actualizar su movimiento sin desactivarlo
            enabled = false;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            DropMoney();
            Die();
        }
    }

    private void DropMoney()
    {
        Debug.Log("DropFunction");
        if (moneyPrefab != null)
        {
            Debug.Log("Suelta dinero");
            Instantiate(moneyPrefab, transform.position + Vector3.up * 1f, Quaternion.identity);
        }
    }



    // Método para desactivar el enemigo al morir
    private void Die()
    {
        gameObject.SetActive(false); // Se desactiva sin destruirse
    }

    public void MakeDamage(int defenseHealth)
    {
        defenseHealth = -damage;
    }



}
