using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistSpell : BaseSpell
{
    [SerializeField] private GameObject _parent;
    [SerializeField] private Animator _animator;
    [SerializeField] private RaycastUnit _raycastUnit;
    void Start()
    {
        cost = 500;
        StartCoroutine(WaitForNext());
    }

    private IEnumerator WaitForNext()
    {
        if (team == 1)
        {
            MessageText.sendMessage?.Invoke("Click on position to deal area damage");
            while (!Input.GetKeyDown(KeyCode.Mouse0))
            {
                Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _parent.transform.position = new Vector3(v.x, 13.5f, _parent.transform.position.z);
                yield return null;
            }
        }
        yield return new WaitForSeconds(Time.deltaTime);
        _animator.SetBool("Attack", true);
    }
    public void GetEnemies()
    {
        Vector2 vect = _parent.transform.position;
        _enemies = _raycastUnit.GetOverlapUnitAll(new Vector2(vect.x - 1.3f, vect.y), new Vector2(vect.x + 1.3f, vect.y - 20), team);
    }
    public void SetPositionX(float x)
    {
        _parent.transform.position = new Vector3(x, 13.5f, _parent.transform.position.z);
    }
}
