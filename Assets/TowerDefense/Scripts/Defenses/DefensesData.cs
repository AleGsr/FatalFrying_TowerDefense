using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DefensesData", menuName = "Scriptable Objects/DefensesData")]
public class DefensesData : ScriptableObject
{
    public string defenseName;
    public float damage;
    public float range;
    public float fireRate;
   
    public GameObject prefab;
    public Sprite icon;

    public int cost;

    public DefenseType type; // Enum: Shooter, Trap, AoE, Upgradable

    public enum DefenseType
    {
        Fries,
        Soda,
        Nugget,
        Seasoning
    }


}
