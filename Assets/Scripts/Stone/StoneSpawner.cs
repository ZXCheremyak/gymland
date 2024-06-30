using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    public static StoneSpawner instance;

    [SerializeField] GameObject[] stones;

    [SerializeField] int spawnRange;

    [SerializeField] int stoneHealthMultiplier;

    [SerializeField]List<Stone> spawnedStones = new List<Stone>();

    void Awake()
    {
        instance = this;

        EventManager.stoneDestroyed.AddListener(Spawn);
    }

    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        spawnedStones.TrimExcess();

        if (spawnedStones.Count > 4) return;

        GameObject newStone = Instantiate(stones[Random.Range(0, stones.Length)], GetRandomSpawnPoint(), Quaternion.identity);

        newStone.GetComponent<Stone>().maxhp *= stoneHealthMultiplier;

        newStone.GetComponent<Stone>().hp = newStone.GetComponent<Stone>().maxhp;


        spawnedStones.Add(newStone.GetComponent<Stone>());


        if (spawnedStones.Count < 5) Spawn();
    }

    Vector2 GetRandomSpawnPoint()
    {
        return new Vector2(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f)) * spawnRange;
    }
}