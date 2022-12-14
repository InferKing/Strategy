using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum UnitStatus
{
    Stay,
    Move,
    Attack,
    Death
}
public enum UnitType
{
    Melee,
    Area,
    Tower,
    Hero
}

public class Unit : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private RaycastUnit _rayUnit;
    [SerializeField] protected GameObject _parent;
    [HideInInspector] public bool isDead = false;
    private Unit _enemy;
    private Tower _tower;
    private float coefAttack = 0.2f;
    public ParticleSystem particleSystem;
    public GameObject healthBar;
    public UnitStatus status;
    public UnitType type;
    public float radius, speed, curSpeed;
    public int health, damage, maxHealth, price, unlockExp; // team should be 1 or 2
    [Range(1, 2)] public int team;
    public bool isLeft;

    private void Start()
    {
        curSpeed = speed;
        if (type is UnitType.Melee)
        {
            radius = _boxCollider.size.x / 2 + 0.1f;
        }
        status = UnitStatus.Move;
        SetAnim();
        StartCoroutine(Life());
    }
    private IEnumerator Life()
    {
        while (status != UnitStatus.Death && health > 0)
        {
            GetEnemy();
            if (status is UnitStatus.Stay)
            {
                StopMove();
                SetAnim();
            }
            else if (status is UnitStatus.Move)
            {
                Move(isLeft);
                SetAnim();
            }
            else if (status is UnitStatus.Attack)
            {
                if (_enemy != null || _tower != null)
                {
                    StopMove();
                    SetAnim();
                    if (_enemy != null || _tower != null)
                    {
                        SetAnim();
                    }
                }
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        status = UnitStatus.Death;
        _boxCollider.enabled = false;
        _rb.gravityScale = 0;
        _rb.velocity = Vector2.zero;
        SetAnim();
        isDead = true;
        yield return new WaitForSeconds(1.5f);
        Destroy(_parent);
    }
    public virtual void Move(bool isLeft)
    {
        _rb.velocity = isLeft ? new Vector2(-1 * curSpeed, _rb.velocity.y) : new Vector2(1 * curSpeed, _rb.velocity.y);
    }
    public virtual void Attack()
    {
        if (_enemy != null && !_enemy.isDead)
        {
            int _damage = (int)UnityEngine.Random.Range(damage * (1 - coefAttack), damage * (1 + coefAttack));
            _enemy.health -= _damage;
            _enemy.health = Mathf.Clamp(_enemy.health, 0, _enemy.maxHealth);
            _enemy.healthBar.transform.localScale = new Vector3(Mathf.Clamp((float)_enemy.health / _enemy.maxHealth, 0, 1), _enemy.healthBar.transform.localScale.y, 1);
            TextController.showUnitUI?.Invoke(_enemy.gameObject, _damage);
            _enemy.particleSystem.Play();
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
                GetEnemy();
                _enemy = null;
            }
        }
    }
    public virtual void AttackTower()
    {
        if (_tower != null)
        {
            Tower.OnTowerAttack?.Invoke(_tower);
            int _damage = (int)UnityEngine.Random.Range(damage * (1 - coefAttack), damage * (1 + coefAttack));
            _tower.health -= _damage;
            _tower.health = Mathf.Clamp(_tower.health, 0, _tower.maxHealth);
            _tower.healthBar.transform.localScale = new Vector3(Mathf.Clamp((float)_tower.health / _tower.maxHealth, 0, 1),
                _tower.healthBar.transform.localScale.y, 1);
            if (_tower.health == 0)
            {
                status = UnitStatus.Move;
                _tower = null;
            }
        }
    }
    private void GetEnemy()
    {
        Unit unit = _rayUnit.GetRaycastUnit(isLeft, radius);
        Tower tower = _rayUnit.GetRaycastTower(isLeft,radius, type);
        if (unit == null)
        {
            _enemy = null;
            if (tower != null && tower.team != team)
            {
                _tower = tower;
                status = UnitStatus.Attack;
                return;
            }
            status = UnitStatus.Move;
            return;
        }
        if (unit.team != team && !unit.isDead)
        {
            status = UnitStatus.Attack;
            _enemy = unit;
        }
        else if (unit.type is UnitType.Melee)
        {
            if (unit.status is UnitStatus.Death)
            {
                status = UnitStatus.Move;
            }
            else
            {
                status = unit.status == UnitStatus.Attack ? UnitStatus.Stay : unit.status;
            }
        }
        else
        {
            status = UnitStatus.Move;
        }
    }
    public void SetAnim()
    {
        _animator.SetBool("Move", status == UnitStatus.Move);
        _animator.SetBool("Idle", status == UnitStatus.Stay);
        _animator.SetBool("Death", status == UnitStatus.Death);
        _animator.SetBool("Attack", status == UnitStatus.Attack);

    }
    public void StopMove()
    {
        _rb.velocity = new Vector2(0, _rb.velocity.y);
    }
    public GameObject GetParent() => _parent;
    public int GetOppositeLayer()
    {
        switch (_parent.layer)
        {
            case 7:
                return 9;
            case 8: 
                return 10;
            case 9:
                return 7;
            case 10:
                return 8;
            default:
                return -1;
        }

    }
    public int[] GetLayersToIgnore(int index)
    {
        switch (index)
        {
            case 7:
                return new int[] {8};
            case 8:
                return new int[] {7,8};
            case 9:
                return new int[] {10};
            case 10:
                return new int[] {9,10};
            default:
                return new int[] {0};
        }

    }
}
