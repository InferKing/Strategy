using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ItemDescription : BaseDescription
{
    [SerializeField] private UnitType _unitType;
    [SerializeField, Range(1, 6)] private int _multiplier; 
    [SerializeField] private int _price, _limitCount;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private bool _isHealth;
    [SerializeField] private Button _button;

    private int _count = 0;
    private void OnEnable()
    {
        ButtonController.UpdateButtonPrices += UpdateDescr;
    }
    private void OnDisable()
    {
        ButtonController.UpdateButtonPrices -= UpdateDescr;
    }
    private void Start()
    {
        _priceText.text = "Price: " + _price.ToString();
    }
    public void UpdateDescr(ItemDescription item)
    {
        if (item != this) return;
        if (IsItLimit())
        {
            MessageText.sendMessage?.Invoke(Constants.LimitReached);
            _button.interactable = false;
            return;
        }
        _count += 1;
        _price *= _multiplier;
        _priceText.text = "Price: " + _price.ToString();
        if (_isHealth) MessageText.sendMessage?.Invoke(Constants.HealthIncreased);
        else MessageText.sendMessage?.Invoke(Constants.AttackIncreased);
    }
    public bool IsItLimit() => _count >= _limitCount-1;
    public int GetPrice() => _price;
    public UnitType GetUnitType() => _unitType;
    public bool IsHealth() => _isHealth;
}
