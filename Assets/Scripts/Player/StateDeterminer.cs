using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class StateDeterminer : MonoBehaviour
{
    [SerializeField] private Tower _enemyTower;
    private Tower _anotherTower;
    public static Action<Tower> OnGetActionTower;
    private void OnEnable()
    {
        OnGetActionTower += SetTower;
    }
    private void OnDisable()
    {
        OnGetActionTower -= SetTower;
    }
    public Vector3 GetEnemyCrowdPos()
    {
        Unit[] units = FindObjectsOfType<Unit>();
        Dictionary<Unit,int> pos = new Dictionary<Unit,int>();
        List<Unit> area = new List<Unit>();
        List<Unit> melee = new List<Unit>();
        foreach (Unit unit in units)
        {
            if (unit.type == UnitType.Area && unit.team == 1)
            {
                area.Add(unit);
                pos.Add(unit, 0);
            }
            else if (unit.type == UnitType.Melee && unit.team == 1)
            {
                melee.Add(unit);
            }
        }
        for (int i = 0; i < area.Count; i++)
        {
            for (int j = i + 1; j < area.Count; j++)
            {
                if ((area[i].gameObject.transform.position-area[j].gameObject.transform.position).magnitude < 1)
                {
                    pos[area[i]] += 1;
                }
            }
        }
        int max = -1;
        Unit un = null;
        foreach (var unit in pos)
        {
            if (unit.Value > max)
            {
                max = unit.Value;
                un = unit.Key;
            }
        }
        if (max > 0) return un.gameObject.transform.position;
        else if (melee.Count != 0) return melee[0].gameObject.transform.position;
        else return Vector3.zero;

    }
    public BotState GetState()
    {
        if (_enemyTower != _anotherTower) return BotState.DefenseRange;
        Unit[] units = FindObjectsOfType<Unit>();
        List<UnitType> types = GetTypes(units);
        switch (types.Count)
        {
            case 0:
                return BotState.AttackMelee;
            case 1:
                if (types[0] is UnitType.Melee)
                {
                    return BotState.AttackRange;
                }
                else
                {
                    return BotState.AttackMelee;
                }
            case 2:
                return BotState.AttackRange;
            default:
                return BotState.AttackMelee;
        }
    }
    private List<UnitType> GetTypes(Unit[] units)
    {
        List<UnitType> types = new List<UnitType>();
        foreach (Unit unit in units)
        {
            if (!types.Contains(unit.type))
            {
                types.Add(unit.type);
            }
        }
        return types;
    }
    private void SetTower(Tower tower)
    {
        _anotherTower = tower;
    }
}
