using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSpell : BaseSpell
{
    [SerializeField] private RaycastUnit _raycastUnit;
    [SerializeField] private ParticleSystem _particleSystem;
    private Vector2 _vect;
    private void Start()
    {
        cost = 800;
        damage = 10;
        StartCoroutine(StartPoison());
    }
    private IEnumerator StartPoison()
    {
        if (team == 1)
        {
            MessageText.sendMessage?.Invoke("Click on position to use poison");
            while (!Input.GetKeyDown(KeyCode.Mouse0))
            {
                yield return null;
            }
            _vect = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        yield return new WaitForSeconds(Time.deltaTime);
        transform.position = new Vector3(_vect.x, 6f, transform.position.z);
        _particleSystem.Play();
        List<Unit> units = new List<Unit>();
        for (int i = 0; i < 17; i++)
        {
            yield return new WaitForSeconds(1f);
            List<Unit> t = _raycastUnit.GetOverlapUnitAll(new Vector2(_vect.x - 4f, _vect.y + 20), new Vector2(_vect.x + 4f, _vect.y - 20), team);
            foreach (var unit in t)
            {
                if (!units.Contains(unit))
                {
                    units.Add(unit);
                }
                Slowdown(unit);
                Attack(unit);
            }
            foreach(var unit in GetNotIntersection(units,t))
            {
                unit.curSpeed = unit.speed;
            }
            
        }
        while (_particleSystem.isPlaying)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
    private void Slowdown(Unit un)
    {
        un.curSpeed = 0.8f * un.speed;
    }
    private List<Unit> GetNotIntersection(List<Unit> t1, List<Unit> t2)
    {
        List<Unit> t = new List<Unit>();
        foreach (var unit in t1)
        {
            if (!t2.Contains(unit))
            {
                t.Add(unit);
            }
        }
        return t;
    }
    public void SetPosX(float x)
    {
        _vect = new Vector2(x, 6f);
    }
}
