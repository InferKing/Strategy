using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class KeyboardSimulator : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private AudioSource _audioSource;
    private bool _isPlaying = false;
    private void OnEnable()
    {
        MainController.ShowFinish += TypeMessage;
    }
    private void OnDisable()
    {
        MainController.ShowFinish -= TypeMessage;
    }
    private void Start()
    {
        _text.text = "";
    }
    public void TypeMessage(int symbPerSeconds, string message)
    {
        if (symbPerSeconds <= 0) return;
        StartCoroutine(CorTypeMessage(symbPerSeconds, message));
        StartCoroutine(CorTypeSound(symbPerSeconds));
    }
    private IEnumerator CorTypeMessage(int symbPerSeconds, string message)
    {
        _isPlaying = true;
        yield return new WaitForSeconds(Time.deltaTime);
        foreach (var i in message)
        {
            _text.text += i;
            yield return new WaitForSeconds(1f/symbPerSeconds);
        }
        _isPlaying = false;
    }
    private IEnumerator CorTypeSound(int symbPerSeconds)
    {
        yield return new WaitForSeconds(Time.deltaTime);
        while (_isPlaying)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.clip = _clips[Random.Range(0, _clips.Length)];
                _audioSource.Play();
            }
            yield return null;
        }
    }
}
