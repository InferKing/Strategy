using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject _spawner;
    [SerializeField] private GameObject[] _menu;
    [SerializeField] private GameObject[] _units;
    [SerializeField] private int[] _priceForTower; // 0 - 
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
            GameObject unit = Instantiate(_units[index]);
            unit.transform.position = _spawner.transform.position;
        }
    }
    public void SetTower(int index)
    {
        if (Singleton.Instance.Player.TryMoneyTransaction(-_units[index].GetComponent<Unit>().price))
        {
            TextController.updatePlayerUI?.Invoke();
            GameObject unit = Instantiate(_units[index]);
            unit.transform.position = _spawner.transform.position;
        }
    }
}
