using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class KeyboardSimulator : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

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
    }
    private IEnumerator CorTypeMessage(int symbPerSeconds, string message)
    {
        foreach (var i in message)
        {
            _text.text += i;
            yield return new WaitForSeconds(1/symbPerSeconds);
        }
    }
}
