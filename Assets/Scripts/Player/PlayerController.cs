using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficult
{
    Easy,
    Medium,
    Hard,
    Impossible
}

public class PlayerController : MonoBehaviour
{
    
}

public class Player
{
    private int money, reputation, experience, towerHealth;
    public Player(int money, int reputation, int experience)
    {
        this.money = money;
        this.reputation = reputation;
        this.experience = experience;
        towerHealth = 2000;
    }
    public bool TryMoneyTransaction(int money)
    {
        if (money > 0 || this.money >= Mathf.Abs(money))
        {
            this.money += money;
            return true;
        }
        return false;
    }
    public void AddExperience(int exp)
    {
        experience += exp;
    }
    public void AddReputation(int rep)
    {
        reputation += rep;
    }
    public void CalculateRep()
    {
        TryMoneyTransaction(5+reputation/10);
    }
    public bool IsAlive()
    {
        return towerHealth > 0;
    }
    public int GetExp() => experience;
    public int GetMoney() => money;
    public int GetRep() => reputation;
    public int GetHealth() => towerHealth;
}

public class Bot : Player
{
    private float _difficult;
    public Bot(float difficult, int money, int reputation, int experience) : base(money, reputation,experience)
    {
        _difficult = difficult;
    }
}

