using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QueueUI : MonoBehaviour
{
    [SerializeField] private Image[] _pos;
    [SerializeField] private Tower _tower;
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private GameObject[] _gObjs;
    private Unit[] _units;
    private Dictionary<Unit, Sprite> _dict;
    private void OnEnable()
    {
        TextController.updatePlayerUI += QueueLogic;
    }
    private void OnDisable()
    {
        TextController.updatePlayerUI -= QueueLogic;
    }
    private void Start()
    {
        _units = new Unit[_gObjs.Length];
        for (int i = 0; i < _gObjs.Length; i++)
        {
            _units[i] = _gObjs[i].GetComponentInChildren<Unit>();
        }
        foreach(var item in _pos)
        {
            item.gameObject.SetActive(false);
        }
        _dict = new Dictionary<Unit, Sprite>();
        for (int i = 0; i < _units.Length; i++)
        {
            _dict.Add(_units[i], _sprites[i]);
        }
    }
    public void QueueLogic()
    {
        if (Singleton.Instance.Player.IsAlive())
        {
            List<GameObject> list = _tower.GetQueue();
            for (int i = 0; i < list.Count && i <_pos.Length; i++)
            {
                Sprite sprite = null;
                if (_dict.TryGetValue(list[i].GetComponentInChildren<Unit>(), out sprite))
                {
                    _pos[i].gameObject.SetActive(true);
                    _pos[i].sprite = sprite; 
                }
            }
            for (int i = list.Count; i < _pos.Length; i++)
            {
                _pos[i].sprite = null;
                _pos[i].gameObject.SetActive(false);
            }
        }
    }
}
