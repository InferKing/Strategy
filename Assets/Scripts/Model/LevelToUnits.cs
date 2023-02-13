using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class DataJson
{
    public List<Dictionary<int, int>> listUnits = new List<Dictionary<int, int>>();
    public List<float> coefs = new List<float>(); 
}

public class LevelToUnits : MonoBehaviour
{
    private string _path = "Assets/Scripts/Model/queue.json";
    public DataJson GetQueueUnits(Difficult dif)
    {
        using StreamReader read = new StreamReader(_path);
        JsonConvert.DeserializeObject<Dictionary<Difficult, DataJson>>(read.ReadToEnd()).TryGetValue(dif, out DataJson ds);
        return ds;
    }
}
