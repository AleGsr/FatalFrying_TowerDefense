using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    private EnemyData data;
    private Transform[] path;
    private int currentIndex;
    float _speed;
    public void Initialize(EnemyData enemyData, Transform[] pathPoints)
    {
        data = enemyData;
        path = pathPoints;
        currentIndex = 0;
        _speed = data.speed;
        transform.position = path[0].position;
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        while (currentIndex < path.Length)
        {
            Vector3 target = path[currentIndex].position;
            while (Vector3.Distance(transform.position, target) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, data.speed * Time.deltaTime);

                Vector3 dir = (target - transform.position).normalized;
                if (dir != Vector3.zero)
                {
                    Quaternion lookRot = Quaternion.LookRotation(dir);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5f);
                }

                yield return null;
            }

            currentIndex++;
        }

        FindObjectOfType<EnemyManager>().RecycleEnemy(gameObject);
    }

    public void StopMoving()
    {
        _speed = 0;
    }

    public void ContinueMoving()
    {
        _speed = data.speed;
    }

}
