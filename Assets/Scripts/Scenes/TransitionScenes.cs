using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScenes : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private float[] _params;
    private bool _active = false;
    public void StartCor(string scene)
    {
        if (!_active)
        {
            StartCoroutine(TransScene(scene));
        }
    }
    private IEnumerator TransScene(string scene)
    {
        _active = true;
        _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, _params[0]);
        _sprite.gameObject.SetActive(true);
        if (_params[0] > _params[1])
        {
            for (float i = _params[0]; i > _params[1]; i -= _params[2])
            {
                _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, i);
                yield return null;
            }
            _sprite.gameObject.SetActive(false);
        }
        else
        {
            for (float i = _params[0]; i < _params[1]; i += _params[2])
            {
                _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, i);
                yield return null;
            }
            SceneManager.StartScene(scene);
        }
        _active = false;
        yield return null;
    }
}
