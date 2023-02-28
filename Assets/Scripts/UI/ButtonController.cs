using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public enum MenuType
{
    Spell,
    Hero,
    Nothing
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
    [SerializeField] private Button[] _heroButtons;
    [SerializeField] private Button[] _cannonButtons;
    [SerializeField] private AudioSource _audioHero;
    [SerializeField] private AudioClip[] _audioSound; // 0 is guitar, 1 is heal
    private List<int> _countUpgrade = new List<int>() { 0, 0, 0, 0 };
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
        MenuType type;
        switch (index)
        {
            case 2:
                type = MenuType.Spell;
                break;
            case 3:
                type = MenuType.Hero;
                break;
            default:
                type = MenuType.Nothing;
                break;
        }
        MenuEnabled?.Invoke(type,_menu[index].activeSelf);
        Singleton.Instance.Player.AddExperience(0);
    }
    public void GetUnit(int index)
    {
        if (!Singleton.Instance.Player.TryMoneyTransaction(-_model.GetUnits()[index].price))
        {
            MessageText.sendMessage?.Invoke(Constants.NoMoney);
            return;
        }
        _tower.AddToQueue(_model.GetUnitsGO()[index]);
        TextController.updatePlayerUI?.Invoke();
    }
    public void SetTower(int index)
    {
        if (_countUpgrade[index] == 5)
        {
            _cannonButtons[index].interactable = false;
            MessageText.sendMessage?.Invoke(Constants.LimitReached);
            return;
        }
        if (!Singleton.Instance.Player.TryMoneyTransaction(-_tower.GetTowerPrices()[index]))
        {
            MessageText.sendMessage?.Invoke(Constants.NoMoney);
            return;
        }
        TextController.updatePlayerUI?.Invoke();
        _countUpgrade[index] += 1;
        switch (index)
        {
            case 0:
                _tower.UpdateCannon(25, 0, 10);
                MessageText.sendMessage?.Invoke(Constants.CannonDamage);
                break;
            case 1:
                _tower.UpdateCannon(0, 20, 0);
                MessageText.sendMessage?.Invoke(Constants.CannonSpeed);
                break;
            case 2:
                _tower.UpdateTower(1, 0);
                MessageText.sendMessage?.Invoke(Constants.TowerRepair);
                break;
            case 3:
                _tower.UpdateTower(0, 500);
                MessageText.sendMessage?.Invoke(Constants.TowerHealth);
                break;
        }
        int x = _tower.GetTowerPrices()[index];
        _tower.GetTowerPrices()[index] = (int)(Mathf.Round(x * 1.5f) / 100) * 100;
        UpdateButtonDescriptions?.Invoke();
    }
    public void SetSpell(int index)
    {
        if (!Singleton.Instance.Player.TryMoneyTransaction(-_model.GetSpells()[index].cost))
        {
            MessageText.sendMessage?.Invoke(Constants.NoMoney);
            return;
        }
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
        if (!Singleton.Instance.Player.TryMoneyTransaction(-item.GetPrice()))
        {
            MessageText.sendMessage?.Invoke(Constants.NoMoney);
            return;
        }
        Unit[] units = _model.GetUnits();
        List<float> list = new List<float>();
        UpgradeStats.bonuses.TryGetValue(item.GetUnitType(), out list);
        if (item.IsHealth())
        {
            list[1] += 0.1f;
        }
        else
        {
            list[0] += 0.2f;
        }
        UpdateButtonPrices?.Invoke(item);
    }
    public void SetHero(int index)
    {
        switch (index)
        {
            case 0:
                if (MainController.hero == null)
                {
                    if (Singleton.Instance.Player.TryMoneyTransaction(-_model.GetHero().price))
                    {
                        if (_audioHero.isPlaying) _audioHero.Stop();
                        _audioHero.clip = _audioSound[0];
                        _audioHero.Play();
                        GameObject gObj = Instantiate(_model.GetHeroGO());
                        gObj.transform.position = _tower.GetSpawnerPos();
                        MainController.hero = gObj.GetComponentInChildren<Hero>();
                        _heroButtons[index].interactable = false;
                    }
                    else MessageText.sendMessage?.Invoke(Constants.NoMoney);
                }
                else MessageText.sendMessage?.Invoke(Constants.HeroOnField);
                break;
            case 1:
                if (MainController.hero != null)
                {
                    if (_audioHero.isPlaying) _audioHero.Stop();
                    _audioHero.clip = _audioSound[1];
                    _audioHero.Play();
                    MainController.hero.StartHeal();
                    _heroButtons[index].interactable = false;
                }
                else MessageText.sendMessage?.Invoke(Constants.HeroNotOnField);
                break;
            case 2:
                if (MainController.hero != null)
                {
                    Debug.Log("work in progress");
                }
                else MessageText.sendMessage?.Invoke(Constants.HeroNotOnField);
                break;
            case 3:
                if (MainController.hero != null)
                {
                    MainController.hero.StartRage();
                    _heroButtons[index-1].interactable = false;
                }
                else MessageText.sendMessage?.Invoke(Constants.HeroNotOnField);
                break;
        }
        Singleton.Instance.Player.AddExperience(0);
    }
}
