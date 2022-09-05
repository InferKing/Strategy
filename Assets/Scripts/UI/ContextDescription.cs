using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ContextDescription : MonoBehaviour
{
    [SerializeField] private GameObject _description;
    [SerializeField] private TMP_Text _descr1, _descr2;
    [SerializeField] private Unit _unit;

    private void Start()
    {
        _descr1.text = $"Price: {_unit.price}\nDamage: {_unit.damage}";
        _descr2.text = $"Health: {_unit.maxHealth}\nAttack speed: {_unit.attackSpeed}";
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
