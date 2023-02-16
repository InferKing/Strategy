using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class FileManager
{
    public static Settings settings;
    public static void SaveSettings(Settings settings)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Constants.pathSettings, FileMode.OpenOrCreate);
        bf.Serialize(file, settings);
    }
    public static void LoadSettings()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Constants.pathSettings))
        {
            FileStream file = File.OpenRead(Constants.pathSettings);
            settings = (Settings)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            FileStream file = File.Create(Constants.pathSettings);
            settings = new Settings
            {
                TotalVolume = 0f,
                MusicVolume = -9f,
                SoundVolume = -29
            };
            bf.Serialize(file, settings);
            file.Close();
        }
    }
}
