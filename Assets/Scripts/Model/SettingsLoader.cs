using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.Audio;
public class SettingsLoader : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    private void Start()
    {
        Debug.Log(Constants.pathSettings);
        FileManager.LoadSettings();
        SetVolumeToAllNoRead(new float[] { FileManager.settings.TotalVolume, FileManager.settings.MusicVolume, FileManager.settings.SoundVolume });
    }
    public void SetVolumeToAllNoRead(float[] vls)
    {
        _audioMixer.SetFloat("master", vls[0]);
        _audioMixer.SetFloat("music", vls[1]);
        _audioMixer.SetFloat("soundFX", vls[2]);
    }
}
