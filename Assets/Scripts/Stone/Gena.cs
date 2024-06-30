using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gena : MonoBehaviour, IHitable
{
    [SerializeField] int powerGrowth;
    [SerializeField] AudioClip[] clips;

    private DamageFlash damageFlash;

    void Start()
    {
        damageFlash = GetComponent<DamageFlash>();
    }

    public void Hit(int bibibbibi)
    {
        Parameters.power += (int)(powerGrowth * Parameters.powerGrowthMultiplier * Parameters.prestigeMultiplier);
        EventManager.powerChanged.Invoke();
        damageFlash.CallDamageFlash();
        EventManager.playSound.Invoke(GetRandomClip(clips));
    }

    AudioClip GetRandomClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }
}
