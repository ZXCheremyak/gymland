using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gena : MonoBehaviour, IHitable
{
    [SerializeField] int powerGrowth;

    public void Hit(int bibibbibi)
    {
        Parameters.power += (int)(powerGrowth * Parameters.powerGrowthMultiplier * Parameters.prestigeMultiplier);
        EventManager.powerChanged.Invoke();
    }
}
