using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandChanger : MonoBehaviour
{
    [SerializeField] Transform newPosition;

    void TravelToIsland(PlayerController player)
    {
        player.transform.position = newPosition.position;
    }
}
