using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public enum MenuType
{
    Spell,
    Hero
}
public class ButtonController : MonoBehaviour
{
    public static Action UpdateButtonDescriptions;
    public static Action<ItemDescription> UpdateButtonPrices;
    public static Action<MenuType, bool> MenuEnabled;
    [SerializeField] private Tower _tower;
    [SerializeField] private GameObject[] _menu;
    [SerializeField] private Model _model;
    [SerializeField] private Button[] _spellButtons;
    private void Start()
    {
        GetMenu(0);
    }
    public void GetMenu(int index)
    {
        for (int i = 0; i < _menu.Length; i++)
        {
            if (i == index) continue;
            if (_menu[i].activeSelf)
            {
                _menu[i].SetActive(false);
            }
        }
        if (_menu.Length > index) _menu[index].SetActive(!_menu[index].activeSelf);
        MenuEnabled?.Invoke(index == 2 ? MenuType.Spell : MenuType.Hero,_menu[2].activeSelf);
        Singleton.Instance.Player.AddExperience(0);
    }
    public void GetUnit(int index)
    {
        if (Singleton.Instance.Player.TryMoneyTransaction(-_model.GetUnits()[index].price))
        {
            _tower.AddToQueue(_model.GetUnitsGO()[index]);
            TextController.updatePlayerUI?.Invoke();
        }
    }
    public void SetTower(int index)
    {
        if (Singleton.Instance.Player.TryMoneyTransaction(-_tower.GetTowerPrices()[index]))
        {
            TextController.updatePlayerUI?.Invoke();
            switch (index)
            {
                case 0:
                    _tower.UpdateCannon(25, 0, 10);
                    MessageText.sendMessage?.Invoke("~~~UPGRADE!~~~\nCannon damage and radius increased");
                    break;
                case 1:
                    _tower.UpdateCannon(0, 20, 0);
                    MessageText.sendMessage?.Invoke("~~~UPGRADE!~~~\nCannon attack speed increased");
                    break;
                case 2:
                    _tower.UpdateTower(1, 0);
                    MessageText.sendMessage?.Invoke("~~~UPGRADE!~~~\nTower repair score increased");
                    break;
                case 3:
                    _tower.UpdateTower(0, 500);
                    MessageText.sendMessage?.Invoke("~~~UPGRADE!~~~\nTower health increased");
                    break;
            }
            int x = _tower.GetTowerPrices()[index];
            _tower.GetTowerPrices()[index] += (int)Mathf.Round(x * 0.5f);
            UpdateButtonDescriptions?.Invoke();
        }
    }
    public void SetSpell(int index)
    {
        if (Singleton.Instance.Player.TryMoneyTransaction(-_model.GetSpells()[index].cost))
        switch (index)
        {
            case 0:
                Instantiate(_model.GetSpellsGO()[index]);
                _spellButtons[index].interactable = false;
                break;
            case 1:
                Instantiate(_model.GetSpellsGO()[index]);
                _spellButtons[index].interactable = false;
                break;
            case 2:
                Instantiate(_model.GetSpellsGO()[index]);
                _spellButtons[index].interactable = false;
                break;
            case 3:
                Instantiate(_model.GetSpellsGO()[index]);
                _spellButtons[index].interactable = false;
                break;
        }
        Singleton.Instance.Player.AddExperience(0);
    }
    public void SetUpgrade(ItemDescription item)
    {
        if (!Singleton.Instance.Player.TryMoneyTransaction(-item.GetPrice())) return;
        Unit[] units = _model.GetUnits();
        for (int i = 0; i < units.Length; i++)
        {
            if (units[i].type == item.GetUnitType())
            {

                if (item.IsHealth())
                {
                    units[i].maxHealth = Mathf.RoundToInt(units[i].maxHealth * 1.1f);
                    units[i].health = units[i].maxHealth;
                }
                else
                {
                    units[i].damage = Mathf.RoundToInt(units[i].damage * 1.2f);
                }
            }
        }
        _model.UpdateUnits(units);
        UpdateButtonPrices?.Invoke(item);
    }
}
