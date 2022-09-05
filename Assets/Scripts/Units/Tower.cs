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
    public void CalculateHealth()
    {
        health += repairCount;
        health = Mathf.Clamp(health, 0, maxHealth);
    }
    public void UpdateCannon(int percDamage, int percSpeed, int percRadius)
    {
        turretDamage += percDamage/100f;
        turretSpeed += percSpeed / 100f;
        turretRadius += percRadius / 100f;
    }
    public float GetTurretDamage() => turretDamage;
    public float GetTurretSpeed() => turretSpeed;
    public float GetTurretRadius() => turretRadius;
}
