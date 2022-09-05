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
    Tower
}

public class Unit : MonoBehaviour
{
    [SerializeField] private GameObject _checkArea;
    [SerializeField] private Rigidbody2D _rb;
    [HideInInspector] public bool isDead = false;
    private Unit _enemy;
    private Tower _tower;
    public GameObject healthBar;
    public UnitStatus status;
    public UnitType type;
    public float attackSpeed, radius;
    public int health, damage, speed, maxHealth, price; // team should be 1 or 2
    [Range(1, 2)] public int team;
    public bool isLeft;


    //public Unit(int health, int damage, int speed, int team, bool isLeft)
    //{
    //    this.health = health;
    //    this.maxHealth = health;
    //    this.damage = damage;
    //    this.speed = speed;
    //    this.team = team;
    //    this.isLeft = isLeft;
    //}

    private void Start()
    {
        StartCoroutine(Life());
    }
    private IEnumerator Life()
    {
        while (status != UnitStatus.Death)
        {
            //Debug.Log($"{gameObject.name} has status {status}");
            GetEnemy();
            if (status is UnitStatus.Stay)
            {
                StopMove();
            }
            else if (status is UnitStatus.Move)
            {
                Move(isLeft);
            }
            else if (status is UnitStatus.Attack)
            {
                if (_enemy != null || _tower != null)
                {
                    StopMove();
                    yield return new WaitForSeconds(attackSpeed);
                    if (_enemy != null)
                    {
                        Attack(_enemy);
                    }
                    else if (_tower != null)
                    {
                        Attack(_tower);
                    }
                }
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public virtual void Move(bool isLeft)
    {
        _rb.velocity = isLeft ? new Vector2(-1 * speed, _rb.velocity.y) : new Vector2(1 * speed, _rb.velocity.y);
    }
    public virtual void Attack(Unit unit)
    {
        int _damage = (int)UnityEngine.Random.Range(damage * 0.8f, damage * 1.2f);
        unit.health -= _damage;
        unit.health = Mathf.Clamp(unit.health, 0, unit.maxHealth);
        unit.healthBar.transform.localScale = new Vector3(Mathf.Clamp((float)unit.health / unit.maxHealth,0,1), unit.healthBar.transform.localScale.y, 1);
        TextController.showUnitUI?.Invoke(unit.gameObject, _damage);
        if (unit.health == 0)
        {
            if (unit.team == 2 && !unit.isDead)
            {
                unit.isDead = true;
                Singleton.Instance.Player.TryMoneyTransaction(unit.price);
                Singleton.Instance.Player.AddExperience(unit.price * 2);
                Singleton.Instance.Player.AddReputation((unit.price / 100)+1);
            }
            TextController.updatePlayerUI?.Invoke();
            unit.status = UnitStatus.Death;
            unit.Die();
            status = UnitStatus.Move;
            _enemy = null;
        }
    }
    public virtual void Attack(Tower unit)
    {
        int _damage = (int)UnityEngine.Random.Range(damage * 0.8f, damage * 1.2f);
        unit.health -= _damage;
        unit.health = Mathf.Clamp(unit.health, 0, unit.maxHealth);
        unit.healthBar.transform.localScale = new Vector3(Mathf.Clamp((float)unit.health / unit.maxHealth, 0, 1), unit.healthBar.transform.localScale.y, 1);
        // TextController.showUnitUI?.Invoke(unit.gameObject, _damage);
        if (unit.health == 0)
        {
            status = UnitStatus.Move;
            _tower = null;
        }
    }
    private void GetEnemy()
    {
        float dist = float.MaxValue;
        Collider2D[] _enemies = Physics2D.OverlapCircleAll(_checkArea.transform.position, radius);
        List<Unit> units = new List<Unit>();
        List<Tower> towers = new List<Tower>();
        Unit unit = null;
        Tower tower = null;
        bool isTeam = false;
        foreach (var enemy in _enemies)
        {
            bool isEnemy = false, isTower = false;
            isEnemy = enemy.TryGetComponent<Unit>(out unit);
            isTower = enemy.TryGetComponent<Tower>(out tower);
            if (isEnemy)
            {
                units.Add(unit);
            }
            else if (isTower)
            {
                towers.Add(tower);
            }
        }
        unit = null;
        foreach (var un in units)
        {
            float newDist = Mathf.Abs((un.gameObject.transform.position - transform.position).magnitude);
            if (newDist < dist && team != un.team)
            {
                dist = newDist;
                unit = un;
            }
            if (type is UnitType.Melee)
            {
                if (newDist < dist && team == un.team && un.status != UnitStatus.Attack && un.status != UnitStatus.Death && un.type != UnitType.Area)
                {
                    status = un.status;
                    isTeam = true;
                }
                else if (newDist < dist && team == un.team && un.status == UnitStatus.Attack && un.type != UnitType.Area)
                {
                    status = UnitStatus.Stay;
                    isTeam = true;
                }
            }
        }
        foreach (var t in towers)
        {
            if (t.team != team)
            {
                _tower = t;
                status = UnitStatus.Attack;
                return;
            }
        }
        if (unit != null)
        {
            _enemy = unit;
            status = UnitStatus.Attack;
        }
        else if (!isTeam)
        {
            status = UnitStatus.Move;
        }
    }

    public void StopMove()
    {
        _rb.velocity = new Vector2(0, _rb.velocity.y);
    }

    public void Die()
    {
        StopCoroutine(Life());
        gameObject.SetActive(false);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.tag == "Front")
    //    {
    //        _enemy = collision.collider.GetComponent<Unit>();
    //        if (_enemy.team != team)
    //        {
    //            status = UnitStatus.Attack;
    //        }
    //    }
    //    else if (collision.collider.tag == "Back")
    //    {
    //        if (_teammate == null)
    //        {
    //            _teammate = collision.collider.GetComponent<Unit>();
    //        }
    //        status = UnitStatus.Stay;
    //    }
    //}
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.collider.tag == "Front")
    //    {
    //        _enemy = collision.collider.GetComponent<Unit>();
    //        if (_enemy.team != team)
    //        {
    //            status = UnitStatus.Attack;
    //        }
    //    }
    //    else if (collision.collider.tag == "Back")
    //    {
    //        status = UnitStatus.Stay;
    //    }
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.collider.tag == "Front")
    //    {
    //        if (_enemy != null) return;
    //        _enemy = null;
    //        status = UnitStatus.Move;
    //    }
    //    else if (collision.collider.tag == "Back")
    //    {
    //        if (_teammate == null)
    //        {
    //            status = UnitStatus.Move;
    //        }
    //        else
    //        {
    //            status = _teammate.status;
    //        }
            
    //    }
    //}
}
