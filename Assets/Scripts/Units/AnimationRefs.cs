using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRefs : MonoBehaviour
{
    public Hero hero;
    public SoundsOnUse soundsOnUse;
    public void Attack()
    {
        hero.Attack();
    }
    public void AttackTower()
    {
        hero.AttackTower();
    }
    public void PlaySound(int index)
    {
        soundsOnUse.PlaySound(index);
    }
}
