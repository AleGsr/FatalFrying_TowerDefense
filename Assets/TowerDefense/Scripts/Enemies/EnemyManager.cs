using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<EnemyData> enemyTypes; 
    private List<GameObject> enemies = new List<GameObject>();

    [SerializeField] private float spawnInterval = 3f; 
    private float timer;

    private PathFinding pathfinding;

    private List<GameObject> activeEnemies = new List<GameObject>();
    private Queue<GameObject> enemyPool = new Queue<GameObject>(); 

    [Header("Condición de Victoria")]
    [SerializeField] private int totalEnemiesToSpawn = 10; 
    private int enemiesSpawned = 0;

    private bool winTriggered = false;

    public TextMeshProUGUI enemyCountText;
    public TextMeshProUGUI enemyTotalText;

    public bool startMoving;
    void Start()
    {
        startMoving = false;
        enemiesSpawned = 0;
        UpdateText();
        pathfinding = FindObjectOfType<PathFinding>();

        if (pathfinding == null)
        {
            Debug.LogError("No se encontró el script PathFinding en la escena.");
        }
    }

    void Update()
    {
        if (startMoving)
        {
            timer += Time.deltaTime;


            if (timer >= spawnInterval && pathfinding.FinalPath.Count > 0 && enemiesSpawned < totalEnemiesToSpawn)
            {
                SpawnEnemies(pathfinding.FinalPath);
                timer = 0f;
            }
        }
       

        // Verifica condición de victoria
        if (!winTriggered && enemiesSpawned >= totalEnemiesToSpawn && activeEnemies.Count == 0)
        {
            Debug.Log("Condición de victoria cumplida.");
            winTriggered = true;
            SceneManager.LoadScene("Win");
        }
    }
    
    public void StartMoving()
    {
        startMoving = true;
    }

    public void SpawnEnemies(List<Vector3> path)
    {
        if (startMoving)
        {
            if (path == null || path.Count == 0)
            {
                Debug.LogError("No hay un camino válido para los enemigos.");
                return;
            }

            EnemyData selectedEnemy = enemyTypes[Random.Range(0, enemyTypes.Count)];

            GameObject enemy;
            if (enemyPool.Count > 0) 
            {
                enemy = enemyPool.Dequeue();
                enemy.SetActive(true); 
                enemy.transform.position = path[0] + Vector3.up * 1.0f;
            }
            else 
            {
                enemy = Instantiate(selectedEnemy.enemyPrefab, path[0] + Vector3.up * 1.0f, Quaternion.identity);
            }

            enemy.GetComponent<Enemy>().Initialize(selectedEnemy, path);
            activeEnemies.Add(enemy);
            enemiesSpawned++;
            UpdateText();
        }
        
    }

    public void RecycleEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        activeEnemies.Remove(enemy);
        enemyPool.Enqueue(enemy); 
    }


    public void RemoveAllEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        enemiesSpawned = 0;
        enemies.Clear();
    }

    public void UpdateText()
    {
        enemyCountText.text = ("" + enemiesSpawned);
        enemyTotalText.text = (" / " + totalEnemiesToSpawn);
    }

 
}
