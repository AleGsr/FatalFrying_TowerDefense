using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;


public class DefensesManager : MonoBehaviour
{
    public GameObject[] prefabsList;
    public PlayerManager playerManager;

    public int idDefense = 0;

    public void PlaceDefense(Vector3 position)
    {
        if (playerManager == null) return;

        switch (idDefense)
        {
            case 1:
                if (playerManager.FriesCost())
                    Instantiate(prefabsList[0], position, Quaternion.identity);
                break;
            case 2:
                if (playerManager.SodaCost())
                    Instantiate(prefabsList[1], position, Quaternion.identity);
                break;
            case 3:
                if (playerManager.NuggetCost())
                    Instantiate(prefabsList[2], position, Quaternion.identity);
                break;
            case 4:
                if (playerManager.SaucesCost())
                    Instantiate(prefabsList[3], position, Quaternion.identity);
                break;
        }
    }

    public void AddFries() { idDefense = 1; }
    public void AddSoda() { idDefense = 2; }
    public void AddNugget() { idDefense = 3; }
    public void AddSauce() { idDefense = 4; }

    void RemoveDefense()
    {
        /*Cuando se tenga activo el bote de basura, entonces al darle clic a 
        una defensa esta se va a desactivar*/
    }

}
