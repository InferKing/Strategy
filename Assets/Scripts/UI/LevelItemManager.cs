using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LevelItemManager : MonoBehaviour
{
    public static Action<LevelItem> pickItem;
    [SerializeField] private LevelItem[] _items;
    private LevelItem _pickedLevelItem;
    private void OnEnable()
    {
        pickItem += UpdateItems;
    }
    private void OnDisable()
    {
        pickItem -= UpdateItems;
    }
    private void Start()
    {
        UpdateItems(null);
    }
    private void UpdateItems(LevelItem item)
    {
        _pickedLevelItem = item;
        foreach (var value in _items)
        {
            if (value == item)
            {
                value.GetParticleSystem().Play();
            }
            else
            {
                value.GetParticleSystem().Stop();
            }
        }
    }
    public void SaveDifficult()
    {
        if (_pickedLevelItem != null)
        {
            PlayerPrefs.SetInt("Difficult", (int)_pickedLevelItem.GetDifficult());
        }
        else PlayerPrefs.SetInt("Difficult", (int)Difficult.Medium);
    }
}
