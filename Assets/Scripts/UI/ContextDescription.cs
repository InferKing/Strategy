using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ContextDescription : MonoBehaviour
{
    [SerializeField] private GameObject _description;
    [SerializeField] private TMP_Text _descr1, _descr2;
    [SerializeField] private Model _model;
    [SerializeField] private int _index;
    private Unit _unit;
    private bool _active = false;
    private void OnEnable()
    {
        if (_active) ShowInfo();
    }
    private void OnDisable()
    {
        if (_active) ShowInfo();
    }
    private void ShowInfo()
    {
        _unit = _model.GetUnits()[_index];
        _descr1.text = $"Price: {_unit.price}\nDamage: {_unit.damage}";
        _descr2.text = $"HP: {_unit.maxHealth}";
    }
    private void Start()
    {
        _active = true;
        ShowInfo();
    }
    public void OnPointerEnter()
    {
        _description.SetActive(true);
    }
    public void OnPointerExit()
    {
        _description.SetActive(false);
    }
}
