using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

public class InternationalText : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SetLang();
    public static InternationalText Instance;
    public string language;
    public TranslateData data;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetLang();
        }
        else Destroy(gameObject);
    }
    public void SetLanguage(string lang)
    {
        language = lang;
        LoadDictionary();
    }
    private void LoadDictionary()
    {
        using (StreamReader r = new StreamReader(Application.persistentDataPath + "/translator.json"))
        {
            string json = r.ReadToEnd();
            data = JsonConvert.DeserializeObject<TranslateData>(json);
        }
    }
}
public class TranslateData
{
    
}
