using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Model : MonoBehaviour
{
    [SerializeField] private GameObject[] _unitsGO;
    [SerializeField] private GameObject[] _spellsGO;
    private Unit[] _units;
    private BaseSpell[] _spells;
    private void Awake()
    {
        _units = new Unit[_unitsGO.Length];
        _spells = new BaseSpell[_spellsGO.Length];
        for (int i = 0; i < _units.Length; i++)
        {
            Unit u = _unitsGO[i].GetComponentInChildren<Unit>();
            _units[i] = u;
        }
        for (int i = 0; i < _spellsGO.Length; i++)
        {
            _spells[i] = _spellsGO[i].GetComponentInChildren<BaseSpell>();
        }
    }
    public GameObject[] GetUnitsGO() => _unitsGO;
    public Unit[] GetUnits() => _units;
    public GameObject[] GetSpellsGO() => _spellsGO;
    public BaseSpell[] GetSpells() => _spells;
    public void UpdateUnit(int index, GameObject newUnit)
    {
        _unitsGO[index] = newUnit;
        _units[index] = _unitsGO[index].GetComponentInChildren<Unit>();
    }
    public void UpdateUnits(Unit[] units)
    {
        units.CopyTo(_units, 0);
    }
}
