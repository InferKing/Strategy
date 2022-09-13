using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class MessageText : MonoBehaviour
{
    public static Action<string> sendMessage;
    [SerializeField] private TMP_Text _textUser;
    private bool _flag = false;
    private void OnEnable()
    {
        sendMessage += ShowMessageUser;
    }
    private void OnDisable()
    {
        sendMessage -= ShowMessageUser;
    }
    private void ShowMessageUser(string message)
    {
        if (!_flag)
        {
            _textUser.text = message;
            _textUser.gameObject.SetActive(true);
            StartCoroutine(ClassicFade(_textUser));
        }
    }

    private IEnumerator ClassicFade(TMP_Text text)
    {
        _flag = true;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        yield return new WaitForSeconds(2f);
        for (float i = 1; i > 0; i -= Time.deltaTime * 2)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, i);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        text.gameObject.SetActive(false);
        _flag = false;
    }
}
