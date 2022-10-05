using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Difficult
{
    Easy,
    Medium,
    Hard,
    Impossible
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private GameObject[] _gObjs;
    [SerializeField] private ButtonController _buttonController;
    private void Update()
    {
        if (Singleton.Instance.Player.IsAlive() && Input.anyKeyDown)
        {
            int index = GetButtonIndex();
            if (index > 0 && index <= _gObjs.Length)
            {
                if (Singleton.Instance.Player.GetExp() >= _gObjs[index-1].GetComponentInChildren<Unit>().unlockExp)
                {
                    if (Singleton.Instance.Player.TryMoneyTransaction(-_gObjs[index - 1].GetComponentInChildren<Unit>().price))
                    {
                        _tower.AddToQueue(_gObjs[index - 1]);
                        TextController.updatePlayerUI?.Invoke();
                    }
                }
            }
            CheckInput();
        }
    }
    private int GetButtonIndex()
    {
        try
        {
            int index = int.Parse(Input.inputString);
            return index;
        }
        catch
        {
            return -1;
        }
    }
    private void CheckInput()
    {
        switch (Input.inputString)
        {
            case "q":
                _buttonController.GetMenu(0);
                break;
            case "w":
                _buttonController.GetMenu(1);
                break;
        }
    }
}

public class Player
{
    protected int money, reputation, experience, towerHealth;
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
    public void SetHealth(int health)
    {
        towerHealth = health;
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