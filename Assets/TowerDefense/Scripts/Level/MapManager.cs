using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class MapManager : MonoBehaviour
{
    public GameObject pathTilePrefab;
    public GameObject wallTilePrefab;
    public GameObject waypointPrefab; // Puedes usar un objeto vacío o con un ícono visible

    public int width = 10;
    public int height = 10;

    private bool[,] isPath;
    public List<Transform> generatedWaypoints = new List<Transform>();
    private List<Vector2Int> orderedPath = new List<Vector2Int>();

    public Transform waypointContainer;


    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemigo in enemigos)
        {
            enemigo.gameObject.SetActive(false);
        }

        // Limpia el mapa anterior si existe
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }
        foreach (Transform wp in waypointContainer)
        {
            DestroyImmediate(wp.gameObject);
        }
        generatedWaypoints.Clear();

        isPath = new bool[width, height];
        GenerateRandomPath();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector3 pos = new Vector3(x, 0, -y);
                if (isPath[x, y])
                {
                    Instantiate(pathTilePrefab, pos, Quaternion.identity, transform);
                }
                else
                {
                    Instantiate(wallTilePrefab, pos, Quaternion.identity, transform);
                }
            }
        }

        // Crear waypoints en orden correcto
        foreach (Vector2Int coords in orderedPath)
        {
            Vector3 pos = new Vector3(coords.x, 0, -coords.y);
            GameObject wp = Instantiate(waypointPrefab, pos + Vector3.up * 0.1f, Quaternion.identity, waypointContainer);
            generatedWaypoints.Add(wp.transform);
        }

        // Enviar los nuevos waypoints al EnemyManager
        FindObjectOfType<EnemyManager>()?.SetPath(generatedWaypoints);
    }

    void GenerateRandomPath()
    {
        orderedPath.Clear();

        int x = 0;
        int y = 0;
        isPath[x, y] = true;
        orderedPath.Add(new Vector2Int(x, y));

        while (x < width - 1 || y < height - 1)
        {
            bool moveHorizontally = Random.value > 0.5f;

            if (moveHorizontally && x < width - 1)
                x++;
            else if (y < height - 1)
                y++;

            isPath[x, y] = true;
            orderedPath.Add(new Vector2Int(x, y));
        }
    }
}
