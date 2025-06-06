using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class MapManager : MonoBehaviour
{
    public GameObject tilePathPrefab;
    public GameObject tileWallPrefab;
    public Transform mapParent;

    public int mapWidth = 10;
    public int mapHeight = 10;

    public List<GameObject> spawnedTiles = new List<GameObject>();
    public List<Transform> waypoints = new List<Transform>();

    private bool[,] pathMap;
    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
  
        foreach (GameObject tile in spawnedTiles)
        {
            if (Application.isPlaying)
                Destroy(tile);
            else
                DestroyImmediate(tile);
        }


        waypoints.Clear();


        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (Application.isPlaying)
                Destroy(enemy);
            else
                DestroyImmediate(enemy);
        }


        pathMap = new bool[mapWidth, mapHeight];

        int currentX = Random.Range(0, mapWidth);
        int currentY = 0;
        pathMap[currentX, currentY] = true;

        while (currentY < mapHeight - 1)
        {
            List<Vector2Int> directions = new List<Vector2Int>();

            if (currentX > 0 && !pathMap[currentX - 1, currentY]) directions.Add(Vector2Int.left);
            if (currentX < mapWidth - 1 && !pathMap[currentX + 1, currentY]) directions.Add(Vector2Int.right);
            if (currentY < mapHeight - 1 && !pathMap[currentX, currentY + 1]) directions.Add(Vector2Int.up);

            if (directions.Count == 0) break;

            Vector2Int dir = directions[Random.Range(0, directions.Count)];
            currentX += dir.x;
            currentY += dir.y;
            pathMap[currentX, currentY] = true;
        }

   
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                Vector3 pos = new Vector3(x, 0, y);
                GameObject tile;

                if (pathMap[x, y])
                {
                    tile = Instantiate(tilePathPrefab, pos, Quaternion.identity, mapParent);
                    waypoints.Add(tile.transform);

                }
                else
                {
                    tile = Instantiate(tileWallPrefab, pos, Quaternion.identity, mapParent);
                }

                spawnedTiles.Add(tile);
            }
        }
    }
}
