using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ContextDescription : MonoBehaviour
{
    [SerializeField] private GameObject _description;
    [SerializeField] private TMP_Text _descr1, _descr2;
    [SerializeField] private GameObject _gObj;
    private Unit _unit;
    private void Start()
    {
        _unit = _gObj.GetComponentInChildren<Unit>();
        _descr1.text = $"Price: {_unit.price}\nDamage: {_unit.damage}";
        _descr2.text = $"HP: {_unit.maxHealth}";
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
