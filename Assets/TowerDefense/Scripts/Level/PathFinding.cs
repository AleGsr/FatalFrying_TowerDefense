using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathFinding : MonoBehaviour
{
    [SerializeField] int gridWidth = 10;
    [SerializeField] int gridDepth = 10;
    [SerializeField] float cellSize = 1f;
    [SerializeField] GameObject cubePrefab;
    [SerializeField] Button resetButton;
    [SerializeField] private EnemyManager enemyManager;

    public List<Vector3> FinalPath { get; private set; } = new List<Vector3>();
    private Dictionary<Vector3, Cell> cells;
    private List<GameObject> spawnedCubes;
    private GameObject gridParent;
    public Vector3 startPoint = new Vector3(0, 0, 0);
    public Vector3 endPoint = new Vector3(9, 0, 9);

    void Start()
    {
        gridParent = new GameObject("GridParent");
        GenerateGrid();
        FindPath(startPoint, endPoint);
        DrawPath();
        enemyManager.SpawnEnemies(FinalPath);
        resetButton.onClick.AddListener(ResetBoard);
    }

    private void GenerateGrid()
    {
        if (spawnedCubes != null)
        {
            foreach (GameObject obj in spawnedCubes)
            {
                Destroy(obj);
            }
        }

        spawnedCubes = new List<GameObject>();
        cells = new Dictionary<Vector3, Cell>();

        for (float x = 0; x < gridWidth; x += cellSize)
        {
            for (float z = 0; z < gridDepth; z += cellSize)
            {
                Vector3 pos = new Vector3(x, 0, z);
                cells.Add(pos, new Cell(pos));

                GameObject cube = Instantiate(cubePrefab, pos, Quaternion.identity, gridParent.transform);
                cube.GetComponent<Renderer>().material.color = Color.gray;
                spawnedCubes.Add(cube);
            }
        }

        if (cells != null)
        {
            for (int i = 0; i < 15; i++)
            {
                Vector3 pos = new Vector3(Random.Range(0, gridWidth), 0, Random.Range(0, gridDepth));
                if (cells.ContainsKey(pos) && pos != startPoint && pos != endPoint)
                {
                    cells[pos].isWall = true;

                    foreach (GameObject cube in spawnedCubes)
                    {
                        if (cube.transform.position == pos)
                        {
                            cube.GetComponent<Renderer>().material.color = Color.black;
                        }
                    }
                }
            }
        }
        else
        {
            Debug.LogError("cells aún no ha sido inicializado.");
        }
    }

    public void FindPath(Vector3 startPos, Vector3 endPos)
    {
        FinalPath = new List<Vector3>();
        if (!cells.ContainsKey(endPos)) return;

        List<Vector3> openSet = new List<Vector3> { startPos };
        HashSet<Vector3> closedSet = new HashSet<Vector3>();

        cells[startPos].gCost = 0;
        cells[startPos].hCost = GetDistance(startPos, endPos);
        cells[startPos].fCost = cells[startPos].gCost + cells[startPos].hCost;

        while (openSet.Count > 0)
        {
            Vector3 currentPos = openSet[0];
            foreach (Vector3 pos in openSet)
            {
                if (cells[pos].fCost < cells[currentPos].fCost ||
                    (cells[pos].fCost == cells[currentPos].fCost && cells[pos].hCost < cells[currentPos].hCost))
                {
                    currentPos = pos;
                }
            }

            openSet.Remove(currentPos);
            closedSet.Add(currentPos);

            if (currentPos == endPos)
            {
                RetracePath(startPos, endPos);
                return;
            }

            foreach (Vector3 neighbor in GetNeighbours(currentPos))
            {
                if (closedSet.Contains(neighbor) || cells[neighbor].isWall) continue;

                int newCost = cells[currentPos].gCost + GetDistance(currentPos, neighbor);
                if (newCost < cells[neighbor].gCost || !openSet.Contains(neighbor))
                {
                    cells[neighbor].gCost = newCost;
                    cells[neighbor].hCost = GetDistance(neighbor, endPos);
                    cells[neighbor].fCost = cells[neighbor].gCost + cells[neighbor].hCost;
                    cells[neighbor].conection = currentPos;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }
    }

    private void RetracePath(Vector3 startPos, Vector3 endPos)
    {
        FinalPath = new List<Vector3>();
        Vector3 currentPos = endPos;

        while (currentPos != startPos)
        {
            FinalPath.Add(currentPos);
            currentPos = cells[currentPos].conection;
        }
        FinalPath.Reverse();
    }

    private List<Vector3> GetNeighbours(Vector3 pos)
    {
        List<Vector3> neighbors = new List<Vector3>();
        Vector3[] directions = { Vector3.right, Vector3.left, Vector3.forward, Vector3.back };

        foreach (Vector3 dir in directions)
        {
            Vector3 neighborPos = pos + dir * cellSize;
            if (cells.ContainsKey(neighborPos))
            {
                neighbors.Add(neighborPos);
            }
        }

        return neighbors;
    }

    private int GetDistance(Vector3 pos1, Vector3 pos2)
    {
        int distX = Mathf.Abs((int)pos1.x - (int)pos2.x);
        int distZ = Mathf.Abs((int)pos1.z - (int)pos2.z);
        return distX + distZ;
    }

    private void DrawPath()
    {
        foreach (Vector3 pos in FinalPath)
        {
            foreach (GameObject cube in spawnedCubes)
            {
                if (cube.transform.position == pos)
                {
                    cube.GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }
    }

    private void ResetBoard()
    {
        enemyManager.RemoveAllEnemies(); // Solo eliminamos enemigos en el reset
        GenerateGrid();
        FindPath(startPoint, endPoint);
        DrawPath();
        enemyManager.SpawnEnemies(FinalPath);
    }

    private class Cell
    {
        public Vector3 position;
        public int gCost = int.MaxValue;
        public int hCost = int.MaxValue;
        public int fCost = int.MaxValue;
        public Vector3 conection;
        public bool isWall;

        public Cell(Vector3 pos)
        {
            position = pos;
        }
    }
}
