using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameters
{
    public static int power = 1;

    public static float powerGrowthMultiplier = 1;

    public static float prestigeMultiplier = 1;

    public static int money = 0;

    public static float sfxVolume;

    public static float musicVolume;

    public static void SaveData()
    {
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.Save();

        Debug.Log(PlayerPrefs.GetFloat("sfxVolume") + " / " + PlayerPrefs.GetFloat("musicVolume"));
    }

    public static void LoadData()
    {
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
        }

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("musicVolume");
        }
    }
}
