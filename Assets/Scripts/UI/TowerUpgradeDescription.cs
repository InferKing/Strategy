using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TowerUpgradeDescription : MonoBehaviour
{
    [SerializeField] private GameObject _description;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Tower _tower;
    [SerializeField] private int _index;

    private void OnEnable()
    {
        ButtonController.UpdateButtonDescriptions += SetText;
    }
    private void OnDisable()
    {
        ButtonController.UpdateButtonDescriptions -= SetText;
    }
    private void Start()
    {
        SetText();
    }

    private void SetText()
    {
        switch (_index)
        {
            case 0:
                _text.text = $"Price: {_tower.GetTowerPrices()[_index]}\nDamage: " +
                    $"{Mathf.Round(_tower.GetTurretDamage())} + {Mathf.Round(_tower.GetTurretDamage() * 0.25f)}";
                break;
            case 1:
                _text.text = $"Price : {_tower.GetTowerPrices()[_index]}\nAS : {Math.Round(_tower.GetTurretSpeed(),2)} + {Math.Round(_tower.GetTurretSpeed() * 0.2f,2)}";
                break;
            case 2:
                _text.text = $"Price : {_tower.GetTowerPrices()[_index]}\nRepair speed : {_tower.GetRepairSpeed()} + 1";
                break;
            case 3:
                _text.text = $"Price : {_tower.GetTowerPrices()[_index]}\nTower HP : {_tower.maxHealth} + 500";
                break;
        }
    }
    public void OnPointerEnter()
    {
        SetText();
        _description.SetActive(true);
    }
    public void OnPointerExit()
    {
        _description.SetActive(false);
    }
}
