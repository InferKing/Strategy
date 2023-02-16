using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    [SerializeField] private Slider[] _sliders;
    [SerializeField] private SettingsLoader _settingsLoader;
    private bool _loaded;
    private void Awake()
    {
        _loaded = false;
        FileManager.LoadSettings();
        _sliders[0].value = FileManager.settings.TotalVolume;
        _sliders[1].value = FileManager.settings.MusicVolume;
        _sliders[2].value = FileManager.settings.SoundVolume;
        _loaded = true;
    }
    public void SetMusic()
    {
        if (!_loaded) return;
        FileManager.settings.TotalVolume = _sliders[0].value;
        FileManager.settings.MusicVolume = _sliders[1].value;
        FileManager.settings.SoundVolume = _sliders[2].value;
        _settingsLoader.SetVolumeToAllNoRead(new float[] { FileManager.settings.TotalVolume, FileManager.settings.MusicVolume, FileManager.settings.SoundVolume });
    }
    public void SaveSettingsButton()
    {
        FileManager.SaveSettings(FileManager.settings);
    }
}
