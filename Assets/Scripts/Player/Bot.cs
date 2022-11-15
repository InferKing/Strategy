using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum BotState
{
    AttackRange,
    AttackMelee,
    DefenseRange,
    DefenseMelee
}
public class Bot : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private StateDeterminer _stateDeterminer;
    private BotState _state;
    // public Difficult difficult { private get; set; }
    private void Start()
    {
        _state = BotState.AttackMelee;
        StartCoroutine(Life());
    }

    private IEnumerator Life()
    {
        while (_tower.health != 0)
        {
            _state = _stateDeterminer.GetState();
            yield return new WaitForSeconds(10);
        }
    }

}
