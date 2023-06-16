using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Tower : MonoBehaviour
{
    public GameObject healthBar;
    public int health, maxHealth;
    [Range(1, 2)] public int team;
    [SerializeField] private int repairCount, limitUnit;
    [SerializeField] private float turretDamage, turretSpeed, turretRadius;
    [SerializeField] private GameObject _spawner;
    [SerializeField] private int[] _priceForTower;
    private List<GameObject> units;
    public static Action<Tower> OnTowerAttack;
    private void OnEnable()
    {
        OnTowerAttack += SendTowerToBot;
    }
    private void OnDisable()
    {
        OnTowerAttack -= SendTowerToBot;
    }
    void Awake()
    {
        units = new List<GameObject>();
    }
    public void CalculateHealth()
    {
        health += repairCount;
        health = Mathf.Clamp(health, 0, maxHealth);
        if (team == 1)
        {
            Singleton.Instance.Player.SetHealth(health);
        }
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
    public void AddToQueue(GameObject unit)
    {
        units.Add(unit);
        if (team == 1) MainController.currentUnits += 1;
    }
    public void RemoveUnit()
    {
        if (units.Count > 0)
        {
            units.RemoveAt(0);
        }
    }
    public bool TrySpawnUnit()
    {
        if (MainController.currentUnits - units.Count < limitUnit && CheckSpawnPos())
        {
            GameObject unit = Instantiate(units[0]);
            unit.transform.position = GetSpawnerPos();
            unit.SetActive(true);
            RemoveUnit();
            if (team == 1) TextController.updatePlayerUI?.Invoke();
            return true;
        }
        return false;
        
    }
    private void SendTowerToBot(Tower tower)
    {
        StateDeterminer.OnGetActionTower?.Invoke(tower);
    }
    private bool CheckSpawnPos()
    {
        if (units[0].GetComponentInChildren<Unit>().type is UnitType.Area) return true;
        RaycastHit2D[] hits = Physics2D.RaycastAll(_spawner.transform.position, 
            team == 1 ? Vector2.right : Vector2.left, units[0].GetComponent<BoxCollider2D>().size.x/2+0.05f);
        foreach(var hit in hits)
        {
            Unit unit = hit.collider.gameObject.GetComponentInChildren<Unit>();
            if (unit != null && unit.type is UnitType.Melee)
            {
                return false;
            }
        }
        return true;
    }
    public float GetTurretDamage() => turretDamage;
    public float GetTurretSpeed() => turretSpeed;
    public float GetTurretRadius() => turretRadius;
    public int GetRepairSpeed() => repairCount;
    public int GetLimitUnit() => limitUnit;
    public Vector3 GetSpawnerPos() => _spawner.transform.position;
    public List<GameObject> GetQueue() => units;
    public int[] GetTowerPrices() => _priceForTower;
}
