using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class VersionUI : MonoBehaviour
{
    [SerializeField] private TMP_Text m_Text;
    void Awake()
    {
        m_Text.text = "Pre-alpha ver. " + Application.version;
    }
}
