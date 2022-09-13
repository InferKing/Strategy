using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BlockSprite : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _exp;
    private bool _flag = false;
    private void Update()
    {
        CheckAndSetSprite();
    }
    private void CheckAndSetSprite()
    {
        if (Singleton.Instance.Player.GetExp() >= _exp && !_flag)
        {
            _flag = true;
            _button.interactable = _flag;
            _image.sprite = _sprite;
            MessageText.sendMessage?.Invoke("~~~NEW UNIT~~~\nUNLOCKED!");
        }
    }
}
