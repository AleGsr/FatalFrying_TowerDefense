using UnityEngine;

public class Trashcan : MonoBehaviour
{

    [SerializeField] GameObject trashcan;
    public void OnMouseDown()
    {
        if (trashcan.gameObject.activeInHierarchy)
        {
            this.gameObject.SetActive(false);
            trashcan.SetActive(false);
        }

    }

}
