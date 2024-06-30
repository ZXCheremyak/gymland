using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] AudioSource sfxSource;

    [SerializeField] AudioSource musicSource;

    [SerializeField] AudioClip[] music;

    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        EventManager.playSound.AddListener(PlaySound);
        musicSource.volume = Parameters.musicVolume;
        sfxSource.volume = Parameters.sfxVolume;
    }

    void Update()
    {
        if (!musicSource.isPlaying)
        {
            //ChangeMusic();
        }
    }

    void ChangeMusic()
    {
        AudioClip clipToPlay = music[Random.Range(1, music.Length)];
        musicSource.clip = clipToPlay;
        musicSource.Play();
    }

    public void ChangeMusicVolume(Slider slider)
    {
        musicSource.volume = slider.value;
    }

    public void ChangeSfxVolume(Slider slider)
    {
        sfxSource.volume = slider.value;
    }

    void PlaySound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
