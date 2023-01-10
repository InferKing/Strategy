using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Unit
{
    private void Start()
    {
        type = UnitType.Hero;
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
        while (Singleton.Instance.Player.IsAlive())
        {
            Vector3 vect = _parent.transform.localScale;
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
    }
}
