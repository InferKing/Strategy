using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Tower _myTower;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private RaycastUnit _raycastUnit;
    private Unit _unit = null;
    private void Update()
    {
        _animator.speed = _myTower.GetTurretSpeed();
        List<Unit> units = _raycastUnit.GetRaycastUnitAll(transform.position, _myTower.team == 2 ? Vector2.left : Vector2.right, _myTower.GetTurretRadius());
        foreach (var unit in units)
        {
            if (unit.team != _myTower.team && !unit.isDead)
            {
                _unit = unit;
                break;
            }
        }
        _animator.SetBool("Attack", _unit != null && !_unit.isDead && _myTower.GetTurretRadius() >= 
            (_myTower.gameObject.transform.position-_unit.gameObject.transform.position).magnitude);
    }
    
    public void Attack() 
    {
        if (_unit != null && !_unit.isDead)
        {
            _audioSource.Play();
            int _damage = (int)Random.Range(_myTower.GetTurretDamage() * 0.8f, _myTower.GetTurretDamage() * 1.2f);
            _unit.health -= _damage;
            _unit.health = Mathf.Clamp(_unit.health, 0, _unit.maxHealth);
            _unit.healthBar.transform.localScale = new Vector3(Mathf.Clamp((float)_unit.health / _unit.maxHealth, 0, 1), _unit.healthBar.transform.localScale.y, 1);
            TextController.showUnitUI?.Invoke(_unit.gameObject, _damage);
            _unit.particleSyst.Play();
            if (_unit.health == 0)
            {
                if (_unit.team == 1 && !_unit.isDead)
                {
                    if (_unit.type != UnitType.Hero) MainController.currentUnits -= 1;
                    TextController.updatePlayerUI?.Invoke();
                }
                if (_unit.team != _myTower.team && !_unit.isDead)
                {
                    _unit.isDead = true;
                    Singleton.Instance.Player.TryMoneyTransaction(_unit.price/2);
                    Singleton.Instance.Player.AddExperience(_unit.price);
                    Singleton.Instance.Player.AddReputation(_unit.price/100+1);
                }
                TextController.updatePlayerUI?.Invoke();
                _unit.status = UnitStatus.Death;
                _unit = null;
            }
        }
    }
}
