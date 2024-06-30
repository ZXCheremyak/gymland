using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    float sfxVolume = 1;
    float musicVolume = 1;

    [SerializeField] AudioSource sfxSource;

    [SerializeField] AudioSource musicSource;

    [SerializeField] AudioClip[] music;

    void Start()
    {
        sfxSource.volume = sfxVolume;
        musicSource.volume = musicVolume;

        EventManager.playSound.AddListener(PlaySound);
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
        AudioClip clipToPlay = music[Random.Range(0, music.Length)];
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
