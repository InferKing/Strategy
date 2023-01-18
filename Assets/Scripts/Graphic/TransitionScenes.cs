using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScenes : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    public void StartCor()
    {
        StartCoroutine(TransScene(1f,0f,0.05f));
    }
    private IEnumerator TransScene(float from, float to, float step)
    {
        _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, from);
        if (from > to)
        {
            for (float i = from; i > to; i -= step)
            {
                _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, i);
                yield return null;
            }
        }
        else
        {
            for (float i = from; i < to; i += step)
            {
                _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, i);
                yield return null;
            }
        }
        // здесь вызов функции смены сцены
        yield return null;
    }
}
