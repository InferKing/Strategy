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
    private Unit[] _units;
    private void Awake()
    {
        _units = new Unit[_gObjs.Length];
        for (int i = 0; i < _gObjs.Length; i++)
        {
            _units[i] = _gObjs[i].GetComponentInChildren<Unit>();
        }
    }
    private void Update()
    {
        if (Singleton.Instance.Player.IsAlive() && Input.anyKeyDown)
        {
            int index = GetButtonIndex();
            if (index > 0 && index <= _units.Length)
            {
                if (Singleton.Instance.Player.GetExp() >= _units[index-1].unlockExp)
                {
                    if (Singleton.Instance.Player.TryMoneyTransaction(-_units[index - 1].price))
                    {
                        _tower.AddToQueue(_units[index - 1]);
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
    private int money, reputation, experience, towerHealth;
    private bool _flag;
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

