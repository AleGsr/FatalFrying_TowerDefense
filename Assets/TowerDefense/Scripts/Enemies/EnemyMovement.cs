using UnityEngine;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    private List<Transform> path;
    private int currentIndex = 0;

    public void SetPath(List<Transform> newPath)
    {
        path = newPath;
        currentIndex = 0;
        transform.position = path[0].position;
    }

    void Update()
    {
        if (path == null || currentIndex >= path.Count) return;

        Vector3 target = path[currentIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            currentIndex++;
        }
    }
}
