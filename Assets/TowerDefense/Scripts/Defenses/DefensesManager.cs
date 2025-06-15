using UnityEngine;

public class DefensesManager : MonoBehaviour
{
    public GameObject[] prefabsList;
    public PlayerManager playerManager;
    public AudioSource putFood;
    public AudioSource putSoda;

    public GameObject friesSelected;
    public GameObject sodaSelected;
    public GameObject nuggetSelected;

    public int idDefense = 0;

    public void PlaceDefense(Vector3 position)
    {
        if (playerManager == null) return;

        switch (idDefense)
        {
            case 1:
                if (playerManager.FriesCost())
                    putFood.Play();
                Instantiate(prefabsList[0], position, Quaternion.identity);
                ResetID();
                friesSelected.gameObject.SetActive(false);
                break;
            case 2:
                if (playerManager.SodaCost())
                    putSoda.Play();
                Instantiate(prefabsList[1], position, Quaternion.identity);
                ResetID();
                sodaSelected.gameObject.SetActive(false);
                break;
            case 3:
                if (playerManager.NuggetCost())
                    putFood.Play();
                Instantiate(prefabsList[2], position, Quaternion.identity);
                ResetID();
                nuggetSelected.gameObject.SetActive(false);
                break;
            case 4:
                if (playerManager.SaucesCost())
                    putFood.Play();
                Instantiate(prefabsList[3], position, Quaternion.identity);
                ResetID();
                break;
        }
    }

    public void AddFries() { idDefense = 1; }
    public void AddSoda() { idDefense = 2; }
    public void AddNugget() { idDefense = 3; }
    public void AddSauce() { idDefense = 4; }

    public void ResetID()
    {
        idDefense = 0;
    }


}
