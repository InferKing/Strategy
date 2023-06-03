using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private ButtonController _buttonController;
    [SerializeField] private Animator _animator;
    [SerializeField] private UnityEngine.UI.Image _blockImage;
    public void PauseGame()
    {
        if (_buttonController.GetTutorial() == 1 || _buttonController.GetTutorial() == -1) return;
        Time.timeScale = 0;
        SetPauseToAnim(true);
        _blockImage.gameObject.SetActive(true);
    }
    public void SetPauseToAnim(bool b)
    {
        _animator.SetBool("Pause", b);
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1;
        _blockImage.gameObject.SetActive(false);
    }
}
