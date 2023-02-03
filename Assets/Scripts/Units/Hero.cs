using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Unit
{
    [SerializeField] private HeroStats _stats;
    public int XP { get; private set; }
    public int Level { get; private set; }
    public int MaxXP { get; private set; }
    private void Start()
    {
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
        while (!isDead)
        {
            Vector3 vect = _parent.transform.localScale;
            _stats.UpdateHeroUI(this);
            if (Input.GetKey(KeyCode.RightArrow))
            {
                _parent.transform.localScale = new Vector3(Mathf.Abs(vect.x),vect.y,vect.z);
                Move(false);
                status = UnitStatus.Move;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                _parent.transform.localScale = new Vector3(-Mathf.Abs(vect.x),vect.y,vect.z);
                Move(true);
                status = UnitStatus.Move;
            }
            else
            {
                StopMove();
                status = UnitStatus.Stay;
            }
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
            MessageText.sendMessage?.Invoke("New hero level!");
        }
    }

}
