using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSpell : BaseSpell
{
    [SerializeField] private int _team;
    [SerializeField] private RaycastUnit _raycastUnit;
    [SerializeField] private ParticleSystem _particleSystem;

    private void Start()
    {
        cost = 800;
        damage = 10;
        StartCoroutine(StartPoison());
    }
    private IEnumerator StartPoison()
    {
        MessageText.sendMessage?.Invoke("Click on position to use poison");
        while (!Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(v.x, 13.5f, transform.position.z);
            yield return null;
        }
        _particleSystem.Play();
        Vector2 vect = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(vect.x, 6f);
        List<Unit> units = new List<Unit>();
        for (int i = 0; i < 17; i++)
        {
            yield return new WaitForSeconds(1f);
            List<Unit> t = _raycastUnit.GetOverlapUnitAll(new Vector2(vect.x - 4f, vect.y), new Vector2(vect.x + 4f, vect.y - 20), _team);
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
}
