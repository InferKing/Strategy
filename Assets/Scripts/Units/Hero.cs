using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hero : Unit
{
    [SerializeField] private HeroStats _stats;
    [SerializeField] private float _healDeltaTime;
    [SerializeField] private int _healAmount;
    private float _eps = 0.05f;
    private Coroutine _cor;
    private bool _enabled = false;
    public int XP { get; private set; }
    public int Level { get; private set; }
    public int MaxXP { get; private set; }
    private void Start()
    {
        coefAT = 1f;
        XP = 0;
        Level = 1;
        MaxXP = 500;
        type = UnitType.Hero;
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
        _enabled = false;
        Vector2 vect = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        status = UnitStatus.Move;
        while (Mathf.Abs(vect.x-transform.position.x) > _eps)
        {
            SetDest(vect.x > _parent.transform.position.x);
            yield return null;
        }
        StopMove();
        status = UnitStatus.Stay;
        _enabled = true;
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
            if (!EventSystem.current.IsPointerOverGameObject() && Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (_cor != null) StopCoroutine(_cor);
                _enabled = false;
                _cor = StartCoroutine(GoTo());
            }
            GetEnemy();
            if (_enabled && status != UnitStatus.Attack) status = UnitStatus.Stay;
            time += Time.deltaTime;
            SetAnim();
            _stats.UpdateHeroUI(this);
            yield return null;
        }
        SetDeath();
        yield return new WaitForSeconds(1.5f);
        _stats.CloseHeroUi();
        Destroy(_parent);
    }
    public void UpdateStats(int cost)
    {
        XP += Mathf.RoundToInt(cost / 2);
        UpdateLevelAndHealth();
        _stats.UpdateHeroUI(this);
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
            damage = Mathf.RoundToInt(damage * 1.2f);
            MessageText.sendMessage?.Invoke(Constants.HeroLvlUp);
        }
    }

}
