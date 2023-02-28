using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NearDeath : MonoBehaviour
{
    [SerializeField] private float _radiusToAddXp;
    public static Action<Unit> OnDeathHappened;

    private void OnEnable()
    {
        OnDeathHappened += AddExpToHero;
    }
    private void OnDisable()
    {
        OnDeathHappened -= AddExpToHero;
    }

    private void AddExpToHero(Unit unit)
    {
        if (MainController.hero == null) return;
        if ((unit.gameObject.transform.position-MainController.hero.transform.position).magnitude < _radiusToAddXp)
        {
            MainController.hero.UpdateStats(Mathf.RoundToInt(unit.price / 2));
        }
    }
}
