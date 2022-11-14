using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastUnit : MonoBehaviour
{
    [SerializeField] private int[] layersToIgnore;
    [SerializeField] private int curLayer;
    private int layerMask;
    private float eps = 0.05f;
    void Start()
    {
        layerMask = TotalMask(layersToIgnore);
    }
    public Unit GetRaycastUnit(bool dir, float radius)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir ? Vector3.left : Vector3.right, 
            radius, ~layerMask);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                Unit unit = hit.collider.gameObject.GetComponentInChildren<Unit>();
                if (unit != null && Mathf.Abs(unit.gameObject.transform.position.x - transform.position.x) > eps)
                {
                    return unit;
                }
            }
        }
        return null;
    }
    public List<Unit> GetRaycastUnitAll(bool dir, float radius)
    {
        List<Unit> result = new List<Unit>();
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir ? Vector3.left : Vector3.right, 
            radius, ~layerMask);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                Unit unit = hit.collider.gameObject.GetComponentInChildren<Unit>();
                if (unit != null && Mathf.Abs(unit.gameObject.transform.position.x - transform.position.x) > eps)
                {
                    result.Add(unit);
                }
            }
        }
        return result;
    }
    public Tower GetRaycastTower(bool dir, float radius, UnitType type)
    {
        Tower result = null;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir ? Vector3.left : Vector3.right, 
            radius, type is UnitType.Melee ? ~(int)Mathf.Pow(2, curLayer) : ~layerMask);
        if (hit.collider != null)
        {
            bool b = hit.collider.gameObject.TryGetComponent<Tower>(out result);
        }
        return result;
    }
    private int TotalMask(int[] layersToIgnore)
    {
        int count = 0;
        foreach (int i in layersToIgnore) count += (int)Mathf.Pow(2,i);
        return count;
    }
}
