using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    public static StoneSpawner instance;

    [SerializeField] GameObject[] stones;

    [SerializeField] int stoneHealthMultiplier;

    [SerializeField] float distance;

    [SerializeField] GameObject spawnedStone;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {

        RespawnStone();
    }

    void RespawnStone()
    {
        if (spawnedStone != null) return;

        GameObject newStone = Instantiate(stones[Random.Range(0, stones.Length)], transform.position, Quaternion.identity);

        newStone.GetComponent<Stone>().maxhp *= stoneHealthMultiplier;

        newStone.GetComponent<Stone>().hp = newStone.GetComponent<Stone>().maxhp;

        spawnedStone = newStone;
        
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.TryGetComponent<PlayerController>(out _))
        {
            RespawnStone();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}