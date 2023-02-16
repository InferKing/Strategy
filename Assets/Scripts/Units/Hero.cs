using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Unit
{
    [SerializeField] private HeroStats _stats;
    [SerializeField] private float _healDeltaTime;
    [SerializeField] private int _healAmount;
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
            Vector3 vect = _parent.transform.localScale;
            _stats.UpdateHeroUI(this);
            if (Input.GetKey(KeyCode.RightArrow))
            {
                _parent.transform.localScale = new Vector3(Mathf.Abs(vect.x),vect.y,vect.z);
                isLeft = false;
                Move(isLeft);
                status = UnitStatus.Move;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                _parent.transform.localScale = new Vector3(-Mathf.Abs(vect.x),vect.y,vect.z);
                isLeft = true;
                Move(isLeft);
                status = UnitStatus.Move;
            }
            else
            {
                StopMove();
                status = UnitStatus.Stay;
            }
            UnitStatus st = status;
            GetEnemy();
            if (status != UnitStatus.Attack)
            {
                status = st;
            }
            time += Time.deltaTime;
            SetAnim();
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
