using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PauseRandomText : MonoBehaviour
{
    [SerializeField] private TMP_Text m_Text;

    public void SetText()
    {
        m_Text.text = Constants.PauseText[Random.Range(0, Constants.PauseText.Count)];
    }
}
