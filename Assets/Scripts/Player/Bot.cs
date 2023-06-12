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
    [SerializeField] private ButtonController _buttonController;
    [SerializeField] private Tower _tower;
    [SerializeField] private StateDeterminer _stateDeterminer;
    [SerializeField] private GameObject[] _units;
    [SerializeField] private Model _model;
    private List<int> _timeToSpell;
    public Difficult difficult { get; private set; }
    private void Start()
    {
        difficult = (Difficult)PlayerPrefs.GetInt("Difficult");
        switch (difficult)
        {
            case Difficult.Easy:
                _timeToSpell = new List<int>() { 60, 105 };
                break;
            case Difficult.Medium:
                _timeToSpell = new List<int>() { 45, 95 };
                break;
            case Difficult.Hard:
                _timeToSpell = new List<int>() { 30, 65 };
                break;
        }
        _tower.UpdateCannon(
            Mathf.RoundToInt((DataQueue.cannonCoefs[difficult][0]-1) * 100), 
            Mathf.RoundToInt((DataQueue.cannonCoefs[difficult][1]-1) * 100), 
            Mathf.RoundToInt((DataQueue.cannonCoefs[difficult][2]-1) * 100)
            );
        _tower.UpdateTower(0, Mathf.RoundToInt(DataQueue.cannonCoefs[difficult][3] - _tower.maxHealth));
        StartCoroutine(Life());
        StartCoroutine(UsingSpell());
    }
    private IEnumerator UsingSpell()
    {
        Dictionary<int, bool> used = new Dictionary<int, bool>() { { 0, false }, { 1, false } };
        List<float> sm = new List<float> { 0, 0 };
        Vector3 vector = Vector3.zero;
        while(_tower.health > 0)
        {
            for (int i = 0; i < sm.Count; i++)
            {
                sm[i] += Time.deltaTime;
                if (used.ContainsValue(false) && !used[i] &&  _model.GetSpells()[i].xp <= Singleton.Instance.Player.GetExp())
                {
                    used[i] = true;
                    sm[i] = 0;
                }
                if (sm[i] > _timeToSpell[i] && used[i])
                {
                    sm[i] = 0;
                    vector = _stateDeterminer.GetEnemyCrowdPos();
                    if (vector != Vector3.zero)
                    {
                        GameObject spell = Instantiate(_model.GetSpellsGO()[i]);
                        switch (i)
                        {
                            case 0:
                                FistSpell f = spell.GetComponentInChildren<FistSpell>();
                                f.SetTeam(2);
                                f.SetPositionX(vector.x);
                                break;
                            case 1:
                                PoisonSpell p = spell.GetComponentInChildren<PoisonSpell>();
                                p.SetTeam(2);
                                p.SetPosX(vector.x);
                                break;
                        }
                    }
                }
            }
            
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private IEnumerator Life()
    {
        yield return new WaitUntil(() => _buttonController.GetTutorial() == -2);
        int count = 0;
        while (_tower.health > 0)
        {
            count = Mathf.Clamp(count, 0, DataQueue.listUnits[difficult].Count-1);
            foreach(var item in DataQueue.listUnits[difficult][count])
            {
                for (int i = 0; i < item.Value; i++)
                {
                    _tower.AddToQueue(_units[item.Key]);
                }
            }
            count++;
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
