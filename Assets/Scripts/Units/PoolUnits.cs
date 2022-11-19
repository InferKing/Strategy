using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolUnits : MonoBehaviour
{
    private int layerArea = 8, layerMelee = 7, limitUnit = 15;
    [SerializeField] private GameObject[] units;
    private List<GameObject> poolUnits = new List<GameObject>();
    private void Awake()
    {
        for (int i = 0; i < limitUnit; i++)
        {
            for (int j = 0; j < units.Length; j++)
            {
                GameObject go = Instantiate(units[j]);
                go.SetActive(false);
                poolUnits.Add(go);
            }
        }
    }
    public GameObject GetUnitOfType(UnitType type)
    {
        foreach (GameObject unit in poolUnits)
        {
            if (!unit.activeSelf && unit.layer == GetMatching(type))
            {
                return unit;
            }
        }
        return null;
    }
    public void SetDestroy(GameObject unit, BoxCollider2D box)
    {
        unit.SetActive(false);
        //box.enabled = true;
    }
    private int GetMatching(UnitType type)
    {
        return type is UnitType.Melee ? layerMelee : layerArea;
    }
}
