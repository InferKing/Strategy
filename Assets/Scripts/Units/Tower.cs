using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject healthBar;
    public int health, maxHealth;
    [Range(1, 2)] public int team;
    [SerializeField] private int repairCount;
    [SerializeField] private float turretDamage, turretSpeed, turretRadius;
    [SerializeField] private GameObject _spawner;
    [SerializeField] private int[] _priceForTower;
    private List<Unit> units;
    void Awake()
    {
        units = new List<Unit>();
    }
    public void CalculateHealth()
    {
        health += repairCount;
        health = Mathf.Clamp(health, 0, maxHealth);
        healthBar.transform.localScale = new Vector3(Mathf.Clamp((float)health / maxHealth, 0, 1), healthBar.transform.localScale.y, 1);
    }
    public void UpdateCannon(int percDamage, int percSpeed, int percRadius)
    {
        turretDamage += percDamage / 100f * turretDamage;
        turretSpeed += percSpeed / 100f * turretSpeed;
        turretRadius += percRadius / 100f * turretRadius;
    }
    public void UpdateTower(int repairCount, int health)
    {
        this.repairCount += repairCount;
        this.health += health;
        this.maxHealth += health;
    }
    public void AddToQueue(Unit unit)
    {
        units.Add(unit);
    }
    public void RemoveUnit()
    {
        if (units.Count > 0)
        {
            units.RemoveAt(0);
        }
    }
    public void SpawnUnit()
    {
        Debug.Log("Here");
        GameObject unit = Instantiate(units[0].gameObject);
        unit.transform.position = GetSpawnerPos();
        RemoveUnit();
    }
    public float GetTurretDamage() => turretDamage;
    public float GetTurretSpeed() => turretSpeed;
    public float GetTurretRadius() => turretRadius;
    public int GetRepairSpeed() => repairCount;
    public Vector3 GetSpawnerPos() => _spawner.transform.position;
    public List<Unit> GetQueue() => units;
    public int[] GetTowerPrices() => _priceForTower;
}
