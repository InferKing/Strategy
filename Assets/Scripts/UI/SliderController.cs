using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderController : MonoBehaviour
{
    [SerializeField] private QueueUI _queueUI;
    [SerializeField] private GameObject _fillArea;
    [SerializeField] private Slider _slider;
    [SerializeField] private Tower _tower;
    private void Start()
    {
        _fillArea.SetActive(false);
        StartCoroutine(Queue());
    }
    private IEnumerator Queue()
    {
        while (Singleton.Instance.Player.IsAlive())
        {
            List<GameObject> queue = _tower.GetQueue();
            if (queue.Count > 0)
            {
                yield return StartCoroutine(Delay());
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    private IEnumerator Delay()
    {
        _fillArea.SetActive(true);
        for (float i = _slider.minValue; i < _slider.maxValue; i += Time.deltaTime)
        {
            _slider.value = Mathf.Clamp(i, _slider.minValue, _slider.maxValue);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        while (!_tower.TrySpawnUnit())
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }
        _queueUI.QueueLogic();
        _fillArea.SetActive(false);
        _slider.value = _slider.minValue;
    }

}
