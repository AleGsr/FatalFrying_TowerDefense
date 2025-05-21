using UnityEngine;

public class Defense : MonoBehaviour
{
    protected DefensesData data;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public virtual void Initialize(DefensesData _data)
    {
        data = _data;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
