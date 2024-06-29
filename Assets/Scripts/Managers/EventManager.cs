using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent stoneDestroyed = new UnityEvent();

    public static UnityEvent<int> moneyChanged = new UnityEvent<int>();

    public static UnityEvent<int> powerChanged = new UnityEvent<int>();

    public static UnityEvent<int> powerGrowthMultChanged = new UnityEvent<int>();

    public static UnityEvent goToNextIsland = new UnityEvent();

}
