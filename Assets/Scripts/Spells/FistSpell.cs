using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistSpell : BaseSpell
{
    [SerializeField] private GameObject _parent;
    [SerializeField] private Animator _animator;
    [SerializeField] private RaycastUnit _raycastUnit;
    [SerializeField] private int _team;
    void Start()
    {
        StartCoroutine(WaitForNext());
    }

    private IEnumerator WaitForNext()
    {
        while (!Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _parent.transform.position = new Vector3(v.x, 13.5f, _parent.transform.position.z);
            yield return null;
        }
        _animator.SetBool("Attack", true);
        Vector2 vect = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _enemies = _raycastUnit.GetOverlapUnitAll(new Vector2(vect.x - 1.3f, vect.y), new Vector2(vect.x + 1.3f, vect.y - 20), _team);
    }
}
