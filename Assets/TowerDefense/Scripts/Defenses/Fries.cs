using UnityEngine;
using System.Collections;

public class Fries : MonoBehaviour
{
    [Header("Duración de vida")]
    public float totalTime = 10f;

    void Start()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward);

        StartCoroutine(Timing());
    }

    private IEnumerator Timing()
    {
        yield return new WaitForSeconds(totalTime);
        gameObject.SetActive(false);
    }
}
