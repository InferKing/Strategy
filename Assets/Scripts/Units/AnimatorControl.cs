using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class AnimatorControl : MonoBehaviour
{
    public static Action<int> IndexChanged;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private int _limit;
    public static int index { get; private set; }
    private void Start()
    {
        index = 0;
    }
    public void SetIndex()
    {
        if (MainController.hero == null) return;
        index += 1;
        if (index >= _limit)
        {
            index = 0;
        }
        _image.sprite = _sprites[index];
        IndexChanged?.Invoke(index);
    }
}
