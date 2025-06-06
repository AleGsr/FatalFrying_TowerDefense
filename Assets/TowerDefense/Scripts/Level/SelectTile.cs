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
        
        CheckTile();
        defensesManager.PlaceDefense(tilePos + Vector3.up * 0.5f);
        if (tileIndex == 0) 
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
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
