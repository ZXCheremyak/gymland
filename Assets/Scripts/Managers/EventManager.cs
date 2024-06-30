using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent<Stone> stoneDestroyed = new UnityEvent<Stone>();

    public static UnityEvent moneyChanged = new UnityEvent();

    public static UnityEvent powerChanged = new UnityEvent();

    public static UnityEvent<int> powerGrowthMultChanged = new UnityEvent<int>();

    public static UnityEvent<AudioClip> playSound = new UnityEvent<AudioClip>();
}
