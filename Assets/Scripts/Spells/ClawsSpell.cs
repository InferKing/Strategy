using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawsSpell : BaseSpell
{
    [SerializeField] private GameObject _parent;
    [SerializeField] private Animator _animator;
    [SerializeField] private RaycastUnit _raycastUnit;
    private Vector2 _vect;
    void Start()
    {
        cost = 1500;
        StartCoroutine(WaitForNext());
    }

    private IEnumerator WaitForNext()
    {
        if (team == 1)
        {
            MessageText.sendMessage?.Invoke("Click on position to kill unit");
            while (!Input.GetKeyDown(KeyCode.Mouse0))
            {
                Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _parent.transform.position = new Vector3(v.x, 10.5f, _parent.transform.position.z);
                yield return null;
            }
            _vect = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        yield return new WaitForSeconds(Time.deltaTime);
        _animator.SetBool("Attack", true);
        _enemies = _raycastUnit.GetOverlapUnitAll(new Vector2(_vect.x - 0.3f, _vect.y), new Vector2(_vect.x + 0.3f, _vect.y - 20), team);
    }
    public void KillEnemy()
    {
        if (_enemies.Count != 0)
        {
            damage = _enemies[0].maxHealth;
            Attack(_enemies[0]);
        }
    }
    public void SetPositionX(int x)
    {
        _parent.transform.position = new Vector3(x, 10.5f, _parent.transform.position.z);
    }
}
