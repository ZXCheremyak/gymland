using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandChanger : MonoBehaviour
{
    [SerializeField] Transform newPosition;
    [SerializeField] int powerRequirement;

    void Start()
    {

    }

    public void TravelToIsland(PlayerController player)
    {
        player.transform.position = newPosition.position;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.TryGetComponent<PlayerController>(out PlayerController player))
        {
            if(Parameters.power >= powerRequirement) TravelToIsland(player);
        }
    }
}
