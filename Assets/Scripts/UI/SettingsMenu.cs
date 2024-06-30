using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] TextMeshProUGUI musicVolume, sfxVolume;

    void Start()
    {
        musicVolume.text = "music: " + (Parameters.musicVolume*100).ToString("0");
        sfxVolume.text = "sound: " + (Parameters.sfxVolume*100).ToString("0");
    }

    public void CloseSettings()
    {
        gameObject.SetActive(false);
        menu.SetActive(true);
    }

    public void SetMusicVolumeText(Slider slider)
    {
        musicVolume.text = "music: " + (slider.value * 100).ToString("0");
    }

    public void SetSfxVolumeText(Slider slider)
    {
        sfxVolume.text = "sound: " + (slider.value * 100).ToString("0");
    }
}
