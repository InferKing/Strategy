using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum LockType
{
    Lock,
    Delay
}

public class BlockSprite : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Button _button;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _sliderFill;
    [SerializeField] private LockType _lockType;
    [SerializeField] private int _exp, _delay;
    [SerializeField] private MenuType _menuType;
    private bool _enabled = false, _once = false;

    private void OnEnable()
    {
        Player.PlayerXPChanged += TrySetSprite;
        Player.PlayerXPChanged += TryDelayInteract;
        ButtonController.MenuEnabled += SetUI;
    }
    private void OnDisable()
    {
        Player.PlayerXPChanged -= TrySetSprite;
        Player.PlayerXPChanged -= TryDelayInteract;
        ButtonController.MenuEnabled -= SetUI;
    }

    private void Start()
    {
        if (_lockType == LockType.Lock)
        {
            TrySetSprite();
        }
        else if (_lockType == LockType.Delay)
        {
            TryDelayInteract();
        }
    }

    private void TrySetSprite()
    {
        if (!_once && Singleton.Instance.Player.GetExp() >= _exp && _lockType == LockType.Lock)
        {
            _once = true;
            _button.interactable = true;
            _image.sprite = _sprite;
            MessageText.sendMessage?.Invoke(Constants.UnitUnlocked);
        }
    }
    private void TryDelayInteract()
    {
        if (!_button.interactable && Singleton.Instance.Player.GetExp() >= _exp && _lockType == LockType.Delay && !_enabled)
        {
            _image.sprite = _sprite;
            StartCoroutine(StartDelay());
        }
    }
    private IEnumerator StartDelay()
    {
        _enabled = true;
        float time = _delay;
        _slider.gameObject.SetActive(true);
        _slider.maxValue = time;
        _slider.minValue = 0;
        _slider.value = _slider.maxValue;
        while (time > 0)
        {
            time -= Time.deltaTime;
            _slider.value = time;
            yield return null;
        }
        _slider.gameObject.SetActive(false);
        _button.interactable = true;
        _enabled = false;
        MessageText.sendMessage?.Invoke(Constants.SpellIsReady);
    }
    private void SetUI(MenuType type, bool enabled)
    {
        if (type == _menuType && _lockType == LockType.Delay)
        {
            _sliderFill.enabled = enabled;
            _image.enabled = enabled;
        }
        else if (_lockType == LockType.Delay)
        {
            _sliderFill.enabled = false;
            _image.enabled = false;
        }
    }
}
