using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderController : MonoBehaviour
{
    [SerializeField] private GameObject _slider, _fillArea;
    [SerializeField] private Tower _tower;
    private void Start()
    {
        StartCoroutine(Queue());
    }
    private IEnumerator Queue()
    {
        while (Singleton.Instance.Player.IsAlive())
        {
            List<Unit> queue = _tower.GetQueue();
            if (queue.Count > 0)
            {
                yield return StartCoroutine(Delay());
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        _tower.SpawnUnit();
    }

}
