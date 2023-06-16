using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class Hero : Unit
{
    public static Action<int> AddXPHero;
    [SerializeField] private Animator[] _animators;
    [SerializeField] private ParticleSystem _particleHeal;
    [SerializeField] private HeroStats _stats;
    [SerializeField] private float _healDeltaTime;
    [SerializeField] private int _healAmount;
    private bool _inRage = false;
    private float _eps = 0.05f;
    private Coroutine _cor;
    private bool _enabled = false;
    public int XP { get; private set; }
    public int Level { get; private set; }
    public int MaxXP { get; private set; }
    private void OnEnable()
    {
        AddXPHero += UpdateStats;
        AnimatorControl.IndexChanged += SetAnimator;
    }
    private void OnDisable()
    {
        AddXPHero -= UpdateStats;
        AnimatorControl.IndexChanged -= SetAnimator;
    }
    private void Start()
    {
        XP = 0;
        Level = 1;
        MaxXP = 500;
        SetAnimator(AnimatorControl.index);
        type = UnitType.Hero;
        _stats = FindObjectOfType<HeroStats>();
        _stats.UpdateHeroUI(this);
        StartCoroutine(FixCor());
    }
    private IEnumerator FixCor()
    {
        yield return null;
        status = UnitStatus.Stay;
        StopMove();
        SetAnim();
        StopAllCoroutines();
        yield return StartCoroutine(UserControl());
    }
    private IEnumerator GoTo()
    {
        _enabled = true;
        Vector2 vect = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        while (Mathf.Abs(vect.x-transform.position.x) > _eps)
        {
            SetDest(vect.x > _parent.transform.position.x);
            status = UnitStatus.Move;
            yield return null;
        }
        StopMove();
        status = UnitStatus.Stay;
        _enabled = false;
    }
    private IEnumerator UserControl()
    {
        curSpeed = speed;
        float time = 0;
        while (!isDead)
        {
            if (time >= _healDeltaTime)
            {
                time = 0;
                health += _healAmount;
                health = Mathf.Clamp(health, 0, maxHealth);
                healthBar.transform.localScale = new Vector3(Mathf.Clamp((float)health / maxHealth, 0, 1), healthBar.transform.localScale.y, 1);
            }
            Unit unit = _rayUnit.GetRaycastUnit(isLeft, radius);
            Tower tower = _rayUnit.GetRaycastTower(isLeft, radius, type);
            if (unit == null && tower == null && !_enabled)
            {
                status = UnitStatus.Stay;
            }
            else if (unit != null || tower != null)
            {
                if (_cor != null && _enabled) StopCoroutine(_cor);
                status = UnitStatus.Attack;
                _enabled = false;
                _enemy = unit;
                _tower = tower;
            }
            if (!EventSystem.current.IsPointerOverGameObject() && Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (_cor != null) StopCoroutine(_cor);
                _cor = StartCoroutine(GoTo());
            }
            
            time += Time.deltaTime;
            SetAnim();
            _stats.UpdateHeroUI(this);
            yield return null;
        }
        SetDeath();
        StopMove();
        StopCoroutine(_cor);
        yield return new WaitForSeconds(2.5f);
        health = 0;
        _stats.UpdateHeroUI(this);
        Destroy(_parent);
    }
    public void SetAnimator(int index)
    {
        _animator = _animators[index];
        _animators[index].gameObject.SetActive(true);
        _animators[1 - index].gameObject.SetActive(false);
        if (_inRage) _animator.speed = 1.5f;
        else _animator.speed = 1f;
        UpdateWeapon(index);
        GetEnemy();
        SetAnim();
    }
    public void UpdateStats(int cost)
    {
        XP += Mathf.RoundToInt(cost / 2);
        UpdateLevelAndHealth();
        _stats.UpdateHeroUI(this);
    }
    private void UpdateWeapon(int index)
    {
        switch (index)
        {
            case 0:
                radius = 1.1f;
                if (_inRage) coefAT = 2.4f;
                else coefAT = 1.4f;
                break;
            case 1:
                radius = 4f;
                if (_inRage) coefAT = 1.65f;
                else coefAT = 0.65f;
                break;
        }
    }
    private void SetDest(bool b)
    {
        Vector3 vect = _parent.transform.localScale;
        _parent.transform.localScale = new Vector3(Mathf.Abs(vect.x) * (isLeft ? -1 : 1), vect.y, vect.z);
        if (b)
        {
            isLeft = false;
        }
        else isLeft = true;
        Move(isLeft);
    }
    private void UpdateLevelAndHealth()
    {
        if (XP >= MaxXP)
        {
            XP -= MaxXP;
            Level += 1;
            MaxXP += 200 * Level;
            maxHealth = 500 + (Level - 1) * 250;
            health += 250;
            damage += 10;
            MessageText.sendMessage?.Invoke(Constants.HeroLvlUp);
        }
    }
    public void StartHeal()
    {
        _particleHeal.Play();
        StartCoroutine(Heal());
    }
    public void StartRage()
    {
        StartCoroutine(Rage());
    }
    private IEnumerator Heal()
    {
        float time = 0f, maxTime = 10f;
        while (time < maxTime)
        {
            time += 0.5f;
            health += Mathf.RoundToInt(maxHealth * 0.01f);
            _stats.UpdateHeroUI(this);
            yield return new WaitForSeconds(0.5f);
        }
    }
    private IEnumerator Rage()
    {
        _inRage = true;
        coefAT += 1f;
        _animator.speed = 1.5f;
        speed = 3f;
        yield return new WaitForSeconds(15f);
        speed = 2f;
        coefAT -= 1f;
        _animator.speed = 1f;
        _inRage = false;
    }
}
