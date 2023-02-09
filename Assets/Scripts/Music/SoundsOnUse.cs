using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsOnUse : MonoBehaviour
{
    // 0 - for button is click sound, for unit is attack sound
    // 1 - for unit is death sound
    [SerializeField] private AudioSource _soundFX;
    [SerializeField] private AudioClip[] _soundFXClip;
    public void PlaySound(int index)
    {
        _soundFX.clip = _soundFXClip[index];
        _soundFX.Play();
    }
}
