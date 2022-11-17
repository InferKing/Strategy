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
    [SerializeField] private GameObject[] _units;
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
            switch (_state)
            {
                case BotState.AttackRange:
                    for (int i = 0; i < 5; i++) _tower.AddToQueue(_units[0]);
                    for (int i = 0; i < 5; i++) _tower.AddToQueue(_units[1]);
                    break;
                case BotState.DefenseRange:
                    for (int i = 0; i < 3; i++) _tower.AddToQueue(_units[0]);
                    for (int i = 0; i < 10; i++) _tower.AddToQueue(_units[1]);
                    break;
                case BotState.AttackMelee:
                    for (int i = 0; i < 7; i++) _tower.AddToQueue(_units[0]);
                    for (int i = 0; i < 3; i++) _tower.AddToQueue(_units[1]);
                    break;
                case BotState.DefenseMelee:
                    for (int i = 0; i < 10; i++) _tower.AddToQueue(_units[0]);
                    for (int i = 0; i < 3; i++) _tower.AddToQueue(_units[1]);
                    break;
            }
            yield return StartCoroutine(SpawnUnit());
            yield return new WaitForSeconds(20);
        }
    }
    private IEnumerator SpawnUnit()
    {
        int x = _tower.GetQueue().Count;
        for (int i = 0; i < x;i++)
        {
            yield return new WaitForSeconds(1);
            while (!_tower.TrySpawnUnit())
            {
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
    }

}
