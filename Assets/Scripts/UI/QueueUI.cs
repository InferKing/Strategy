using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QueueUI : MonoBehaviour
{
    [SerializeField] private Image[] _pos;
    [SerializeField] private Tower _tower;
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private Model _model;
    private Dictionary<GameObject, Sprite> _dict;
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
        foreach(var item in _pos)
        {
            item.gameObject.SetActive(false);
        }
        _dict = new Dictionary<GameObject, Sprite>();
        for (int i = 0; i < _model.GetUnitsGO().Length; i++)
        {
            _dict.Add(_model.GetUnitsGO()[i], _sprites[i]);
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
                if (_dict.TryGetValue(list[i], out sprite))
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
