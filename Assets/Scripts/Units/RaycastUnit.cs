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
    public List<Unit> GetRaycastUnitAll(Vector2 from, Vector2 dir, float radius)
    {
        List<Unit> result = new List<Unit>();

        RaycastHit2D[] hits = Physics2D.RaycastAll(from, dir, radius, ~layerMask);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                Unit unit = hit.collider.gameObject.GetComponentInChildren<Unit>();
                if (unit != null && Mathf.Abs(unit.gameObject.transform.position.x - from.x) > eps)
                {
                    result.Add(unit);
                }
            }
        }
        return result;
    }
    public List<Unit> GetOverlapUnitAll(Vector2 point1, Vector2 point2, int team)
    {
        List<Unit> result = new List<Unit>();
        Collider2D[] hits = Physics2D.OverlapAreaAll(point1, point2);
        foreach (Collider2D hit in hits)
        {
            Unit unit = hit.gameObject.GetComponentInChildren<Unit>();
            if (unit != null && team != unit.team)
            {
                result.Add(unit);
            }
        }
        return result;
    }
    public Tower GetRaycastTower(bool dir, float radius, UnitType type)
    {
        Tower result = null;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir ? Vector3.left : Vector3.right, 
            radius, type is UnitType.Melee ? ~((int)Mathf.Pow(2, curLayer)+(int)Mathf.Pow(2, 
            curLayer == 7 || curLayer == 8 ? 8 : 10)) : ~layerMask);
        if (hit.collider != null)
        {
            hit.collider.gameObject.TryGetComponent<Tower>(out result);
        }
        return result;
    }
    private int TotalMask(int[] layersToIgnore)
    {
        int count = 0;
        foreach (int i in layersToIgnore) count += (int)Mathf.Pow(2,i);
        return count;
    }
    public void UpdateParams(int[] layers, int curLayer)
    {
        layersToIgnore = layers;
        this.curLayer = curLayer;
        layerMask = TotalMask(layersToIgnore);
    }
}
