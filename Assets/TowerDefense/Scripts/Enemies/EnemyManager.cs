using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public EnemyData[] enemyTypes;
    public Transform spawnPoint;
    public Transform[] pathPoints;

    private Queue<GameObject> enemyPool = new Queue<GameObject>();

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 2f, 2f);
    }

    void SpawnEnemy()
    {
        int index = Random.Range(0, enemyTypes.Length);
        var data = enemyTypes[index];

        GameObject enemyObj;
        if (enemyPool.Count > 0)
        {
            enemyObj = enemyPool.Dequeue();
            enemyObj.SetActive(true);
        }
        else
        {
            enemyObj = Instantiate(data.prefab);
        }

        enemyObj.GetComponent<EnemyMovement>().Initialize(data, pathPoints);
    }

    public void RecycleEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy);
    }


    public void SetPath(List<Transform> newPath)
    {
        pathPoints = newPath.ToArray();
    }

}
