using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gena : MonoBehaviour, IHitable
{
    [SerializeField] int powerGrowth;
    [SerializeField] AudioClip[] clips;
    public void Hit(int bibibbibi)
    {
        Parameters.power += (int)(powerGrowth * Parameters.powerGrowthMultiplier * Parameters.prestigeMultiplier);
        EventManager.powerChanged.Invoke();
        EventManager.playSound.Invoke(GetRandomClip(clips));
    }

    AudioClip GetRandomClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }
}
