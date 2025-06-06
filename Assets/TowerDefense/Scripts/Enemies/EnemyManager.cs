using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<EnemyData> enemyTypes; // Lista con los 3 tipos de enemigos
    private List<GameObject> enemies = new List<GameObject>();

    [SerializeField] private float spawnInterval = 3f; // Genera enemigos cada 3 segundos
    private float timer;

    private PathFinding pathfinding;

    private List<GameObject> activeEnemies = new List<GameObject>();
    private Queue<GameObject> enemyPool = new Queue<GameObject>(); // Pool de enemigos reutilizables


    void Start()
    {
        pathfinding = FindObjectOfType<PathFinding>();

        if (pathfinding == null)
        {
            Debug.LogError("No se encontró el script PathFinding en la escena.");
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval && pathfinding.FinalPath.Count > 0)
        {
            SpawnEnemies(pathfinding.FinalPath);
            timer = 0f; // Reiniciar el tiempo de spawn
        }
    }

    public void SpawnEnemies(List<Vector3> path)
    {
        if (path == null || path.Count == 0)
        {
            Debug.LogError("No hay un camino válido para los enemigos.");
            return;
        }

        EnemyData selectedEnemy = enemyTypes[Random.Range(0, enemyTypes.Count)];

        GameObject enemy;
        if (enemyPool.Count > 0) // Si hay enemigos en el pool, reutilizar
        {
            enemy = enemyPool.Dequeue();
            enemy.SetActive(true); // Activarlo
            enemy.transform.position = path[0] + Vector3.up * 1.0f;
        }
        else // Si no hay enemigos en el pool, crear nuevos
        {
            enemy = Instantiate(selectedEnemy.enemyPrefab, path[0] + Vector3.up * 1.0f, Quaternion.identity);
        }

        enemy.GetComponent<Enemy>().Initialize(selectedEnemy, path);
        activeEnemies.Add(enemy);
    }

    public void RecycleEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        activeEnemies.Remove(enemy);
        enemyPool.Enqueue(enemy); // Enviar al pool para reutilizarlo
    }

    public void RemoveAllEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        enemies.Clear();
    }
}
