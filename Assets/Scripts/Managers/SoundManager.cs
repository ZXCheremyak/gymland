using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] AudioSource sfxSource;

    [SerializeField] AudioSource musicSource;

    [SerializeField] AudioSource ambientSource;

    [SerializeField] AudioClip[] music;

    [SerializeField] AudioClip ambient;

    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        Parameters.LoadData();
        Debug.Log(PlayerPrefs.GetFloat("sfxVolume") + " / " + PlayerPrefs.GetFloat("musicVolume"));
        EventManager.playSound.AddListener(PlaySound);
        musicSource.volume = Parameters.musicVolume;
        sfxSource.volume = Parameters.sfxVolume;
        ambientSource.volume = Parameters.sfxVolume;
    }

    void Update()
    {
        if (!musicSource.isPlaying)
        {
            ChangeMusic();
        }
    }

    void ChangeMusic()
    {
        AudioClip clipToPlay = music[Random.Range(0, music.Length)];
        musicSource.clip = clipToPlay;
        musicSource.Play();
    }

    public void ChangeMusicVolume(Slider slider)
    {
        musicSource.volume = slider.value;
        Parameters.musicVolume = slider.value;
        Parameters.SaveData();
    }

    public void ChangeSfxVolume(Slider slider)
    {
        sfxSource.volume = slider.value;
        ambientSource.volume = slider.value;
        Parameters.sfxVolume = slider.value;
        Parameters.SaveData();
    }

    void PlaySound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
