using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStats : MonoBehaviour 
{
    // List<float> : 0 index is bonus damage, 1 is bonus health
    public static Dictionary<UnitType, List<float>> bonuses = new Dictionary<UnitType, List<float>>();
    public static Dictionary<UnitType, List<float>> bonusesEnemy = new Dictionary<UnitType, List<float>>();
    private void Awake()
    {
        UpgradeStats[] st = FindObjectsOfType<UpgradeStats>();
        foreach (UpgradeStats st2 in st)
        {
            if (st2 != this) Destroy(st2.gameObject);
        }
        bonuses = new Dictionary<UnitType, List<float>>()
        {
            [UnitType.Melee] = new List<float>() { 1, 1 },
            [UnitType.Area] = new List<float>() { 1, 1 }
        };
        bonusesEnemy = new Dictionary<UnitType, List<float>>()
        {
            [UnitType.Melee] = new List<float>() { 1, 1 },
            [UnitType.Area] = new List<float>() { 1, 1 }
        };
    }
}
