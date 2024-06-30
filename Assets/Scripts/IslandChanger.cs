using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandChanger : MonoBehaviour
{
    [SerializeField] Transform newPosition;
    [SerializeField] int powerRequirement;
    PlayerController player;

    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
    }

    public void TravelToIsland()
    {
        if (Parameters.power >= powerRequirement)
            player.transform.position = newPosition.position;
    }
}
