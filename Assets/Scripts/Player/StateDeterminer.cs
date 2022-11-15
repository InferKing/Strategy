using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/* ����������� ��������� ��� ���� ������������ �� ��������� ���������
 * 1. �����: ����� ��������, ���� ���������. ���: ����� ������� ������, ������� ��������.
 * 2. �����: ���� ��������, ����� ���������. ���: ������� ������� ���������, ����� ��������.
 * 3. �����: ������� ������� ���������, ����� ��������. ���: ����� ����-������� ������, ������� ��������.
 * 4. �����: ������ ��������. ���: ������� ������� ���������, ����� ��������.
 * 5. �����: ������ �������. ���: ������ ������� �����.
 * 6. �����: ����� ���� ���� ����. ���: ����� ��������, ���� ������� ���������.
 * 7. �����: ������ ���. ���: ��������� � �������� �������.
 */
public class StateDeterminer : MonoBehaviour
{
    [SerializeField] private Tower _enemyTower;
    private List<GameObject> _enemies;
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
