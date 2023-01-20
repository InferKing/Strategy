using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawsSpell : BaseSpell
{
    [SerializeField] private GameObject _parent;
    [SerializeField] private Animator _animator;
    [SerializeField] private RaycastUnit _raycastUnit;
    [SerializeField] private int _team;
    void Start()
    {
        cost = 1500;
        StartCoroutine(WaitForNext());
    }

    private IEnumerator WaitForNext()
    {
        MessageText.sendMessage?.Invoke("Click on position to kill unit");
        while (!Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _parent.transform.position = new Vector3(v.x, 10.5f, _parent.transform.position.z);
            yield return null;
        }
        _animator.SetBool("Attack", true);
        Vector2 vect = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _enemies = _raycastUnit.GetOverlapUnitAll(new Vector2(vect.x - 0.3f, vect.y), new Vector2(vect.x + 0.3f, vect.y - 20), _team);
    }
    public void KillEnemy()
    {
        if (_enemies.Count != 0)
        {
            damage = _enemies[0].maxHealth;
            Attack(_enemies[0]);
        }
    }
}
