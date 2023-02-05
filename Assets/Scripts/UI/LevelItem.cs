using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelItem : MonoBehaviour
{
    [SerializeField] private GameObject _unit;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Difficult _difficult;
    public void OnMouseEnter()
    {
        Unit unit = _unit.GetComponentInChildren<Unit>();
        unit.status = UnitStatus.Attack;
        unit.SetAnim();
    }
    public void OnMouseDown()
    {
        LevelItemManager.pickItem?.Invoke(this);
    }
    public void OnMouseExit()
    {
        Unit unit = _unit.GetComponentInChildren<Unit>();
        unit.status = UnitStatus.Stay;
        unit.SetAnim();
    }
    public ParticleSystem GetParticleSystem() => _particleSystem;
    public Difficult GetDifficult() => _difficult;
}
