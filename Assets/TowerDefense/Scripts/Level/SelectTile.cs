using UnityEngine;

public class SelectTile : MonoBehaviour
{
    DefensesManager defensesManager;
    int tileIndex;
    float tilePosX;
    float tilePosZ;
    Vector3 tilePos;

    void Start()
    {
        tilePosX = transform.position.x;
        tilePosZ = transform.position.z;
        tilePos = new Vector3(tilePosX, 0, tilePosZ);
        defensesManager = FindObjectOfType<DefensesManager>();
    }

    private void OnMouseDown()
    {
        if (tileIndex == 1) 
        {
            defensesManager.PlaceDefense(tilePos + Vector3.up * 0.5f); 
        }
    }

    private void OnMouseEnter()
    {
        CheckTile();
        GetComponent<Renderer>().material.color = Color.blue; 
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    void CheckTile()
    {
        if (CompareTag("Path"))
        {
            tileIndex = 0;
        }
        else if (CompareTag("Wall"))
        {
            tileIndex = 1;
        }
    }
}
