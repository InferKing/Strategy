using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonController : MonoBehaviour
{
    public static Action UpdateButtonDescriptions;
    [SerializeField] private Tower _tower;
    [SerializeField] private GameObject[] _menu;
    [SerializeField] private GameObject[] _units;
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
    }
    public void GetUnit(int index)
    {
        if (Singleton.Instance.Player.TryMoneyTransaction(-_units[index].GetComponent<Unit>().price))
        {
            TextController.updatePlayerUI?.Invoke();
            _tower.AddToQueue(_units[index].GetComponent<Unit>());
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
}
