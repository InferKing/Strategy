using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public void PauseGame()
    {
        Time.timeScale = 0;
        SetPauseToAnim(true);
    }
    public void SetPauseToAnim(bool b)
    {
        _animator.SetBool("Pause", b);
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1;
    }
}
