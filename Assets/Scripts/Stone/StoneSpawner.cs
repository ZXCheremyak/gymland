using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    public static StoneSpawner instance;

    [SerializeField] GameObject[] stones;

    [SerializeField] Transform parentPoint;

    [SerializeField] int spawnRange;

    [SerializeField] int stoneHealthMultiplier;

    [SerializeField] float distance;

    [SerializeField]List<Stone> spawnedStones = new List<Stone>();
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        RespawnAllStones();
    }

    void RespawnAllStones()
    {
        foreach (var stone in spawnedStones)
        {
            if(stone != null) Destroy(stone.gameObject);
        }

        spawnedStones.Clear();

        for (int i = 0; i < 5; i++)
        {
            GameObject newStone = Instantiate(stones[Random.Range(0, stones.Length)], parentPoint.GetChild(i).position, Quaternion.identity);

            newStone.GetComponent<Stone>().maxhp *= stoneHealthMultiplier;

            newStone.GetComponent<Stone>().hp = newStone.GetComponent<Stone>().maxhp;

            spawnedStones.Add(newStone.GetComponent<Stone>());
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.TryGetComponent<PlayerController>(out _))
        {
            RespawnAllStones();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}