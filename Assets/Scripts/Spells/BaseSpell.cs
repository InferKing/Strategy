using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpell : MonoBehaviour
{
    protected int damage = 100;
    protected float coefAttack = 0;
    protected List<Unit> _enemies = new List<Unit>();

    protected virtual void Attack(Unit _enemy)
    {
        if (_enemy != null && !_enemy.isDead)
        {
            int _damage = (int)Random.Range(damage * (1 - coefAttack), damage * (1 + coefAttack));
            _enemy.health -= _damage;
            _enemy.health = Mathf.Clamp(_enemy.health, 0, _enemy.maxHealth);
            _enemy.healthBar.transform.localScale = new Vector3(Mathf.Clamp((float)_enemy.health / _enemy.maxHealth, 0, 1),
                _enemy.healthBar.transform.localScale.y, 1);
            TextController.showUnitUI?.Invoke(_enemy.gameObject, _damage);
            if (_enemy.health == 0)
            {
                if (_enemy.team == 2 && !_enemy.isDead)
                {
                    _enemy.isDead = true;
                    Singleton.Instance.Player.TryMoneyTransaction(_enemy.price);
                    Singleton.Instance.Player.AddExperience(_enemy.price * 2);
                    Singleton.Instance.Player.AddReputation((_enemy.price / 100) + 1);
                }
                else if (_enemy.team == 1 && !_enemy.isDead)
                {
                    _enemy.isDead = true;
                    MainController.currentUnits -= 1;
                    TextController.updatePlayerUI?.Invoke();
                }
                TextController.updatePlayerUI?.Invoke();
                _enemy.status = UnitStatus.Death;
            }
        }
    }
    protected void MassiveAttack()
    {
        foreach (Unit unit in _enemies)
        {
            Attack(unit);
        }
    }
}
