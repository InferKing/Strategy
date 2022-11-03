using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastUnit : MonoBehaviour
{
    [SerializeField] private int[] layersToIgnore;
    private int layerMask;
    private float eps = 0.05f;
    void Start()
    {
        layerMask = TotalMask();
    }
    public Unit GetRaycastUnit(bool dir, float radius)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir ? -transform.right : transform.right, radius, ~layerMask);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                Unit unit = hit.collider.gameObject.GetComponentInChildren<Unit>();
                if ((unit.gameObject.transform.position - transform.position).magnitude > eps)
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
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir ? -transform.right : transform.right, radius, ~layerMask);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                Unit unit = hit.collider.gameObject.GetComponentInChildren<Unit>();
                if ((unit.gameObject.transform.position - transform.position).magnitude > eps)
                {
                    result.Add(unit);
                }
            }
        }
        return result;
    }
    private int TotalMask()
    {
        int count = 0;
        foreach (int i in layersToIgnore) count += (int)Mathf.Pow(2,i);
        return count;
    }
}
