using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HeroStats : MonoBehaviour
{
    [SerializeField] private GameObject _main;
    [SerializeField] private Slider _sliderHP;
    [SerializeField] private Slider _sliderXP;
    [SerializeField] private TMP_Text _status;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _hp;
    [SerializeField] private TMP_Text _xp;

    public void UpdateHeroUI(Hero hero)
    {
        _status.text = "Status: " + hero.status.ToString();
        _level.text = "Level: " + hero.Level.ToString();
        _hp.text = hero.health.ToString() + "/" + hero.maxHealth.ToString();
        _xp.text = hero.XP.ToString() + "/" + hero.MaxXP.ToString();
        _sliderHP.maxValue = hero.maxHealth;
        _sliderHP.value = hero.health;
        _sliderXP.maxValue = hero.MaxXP;
        _sliderXP.value = hero.XP;
    }
}
