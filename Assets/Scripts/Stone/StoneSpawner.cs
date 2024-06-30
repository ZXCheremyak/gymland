using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    public static StoneSpawner instance;

    [SerializeField] GameObject stone;

    [SerializeField] int stoneHealthMultiplier;

    [SerializeField] int bountyMultiplier;

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

        GameObject newStone = Instantiate(stone, transform.position, Quaternion.identity);

        newStone.GetComponent<Stone>().maxhp *= stoneHealthMultiplier;

        newStone.GetComponent<Stone>().bounty *= bountyMultiplier;

        newStone.GetComponent<Stone>().hp = newStone.GetComponent<Stone>().maxhp;

        newStone.GetComponent<Stone>().spawner = gameObject;

        spawnedStone = newStone;
        
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.TryGetComponent<PlayerController>(out _))
        {
            //RespawnStone();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, distance);
    }

    public void RequestRespawn()
    {
        Invoke("RespawnStone", 3f);
    }
}