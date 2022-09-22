using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Tower _myTower;
    [SerializeField] private Animator _animator;
    private Unit _unit;
    private void Update()
    {
        _animator.speed = _myTower.GetTurretSpeed();
        GetUnit();
    }
    private void GetUnit()
    {
        if (Singleton.Instance.Player.IsAlive())
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, _myTower.team == 1 ? Vector2.right : Vector2.left, _myTower.GetTurretRadius());
            for(int i = hits.Length-1; i >= 0; i--)
            {
                if (hits[i].collider != null)
                {
                    Unit unit = null;
                    unit = hits[i].collider.gameObject.GetComponentInChildren<Unit>();
                    if (unit != null && unit.team != _myTower.team)
                    {
                        _unit = unit;
                    }
                }
            }
            _animator.SetBool("Attack", _unit != null && hits.Length > 1);
        }
        
    }
    public void Attack() 
    {
        if (_unit != null)
        {
            int _damage = (int)Random.Range(_myTower.GetTurretDamage() * 0.8f, _myTower.GetTurretDamage() * 1.2f);
            _unit.health -= _damage;
            _unit.health = Mathf.Clamp(_unit.health, 0, _unit.maxHealth);
            _unit.healthBar.transform.localScale = new Vector3(Mathf.Clamp((float)_unit.health / _unit.maxHealth, 0, 1), _unit.healthBar.transform.localScale.y, 1);
            TextController.showUnitUI?.Invoke(_unit.gameObject, _damage);
            if (_unit.health == 0)
            {
                if (_unit.team == 1 && !_unit.isDead)
                {
                    MainController.currentUnits -= 1;
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
