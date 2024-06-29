using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneManager : MonoBehaviour
{
    public static StoneManager instance;

    [SerializeField]
    string[] nameStarts;

    [SerializeField]
    string[] nameMiddles;

    [SerializeField]
    string[] nameEndings;

    string smallStoneName;

    string midStoneName;

    string largeStoneName;

    [SerializeField] GameObject[] stones;

    public static int stoneHpMultiplier = 1, stoneBountyMultiplier = 1;

    void Awake()
    {
        instance = this;

        EventManager.stoneDestroyed.AddListener(Spawn);
        EventManager.goToNextIsland.AddListener(OnNextIsland);
    }

    void Start()
    {
        RandomizeNames();
    }

    void RandomizeNames()
    {
        smallStoneName = nameStarts[Random.Range(0, nameStarts.Length)] + nameMiddles[Random.Range(0, nameMiddles.Length)] + nameEndings[Random.Range(0, nameEndings.Length)];
        midStoneName = nameStarts[Random.Range(0, nameStarts.Length)] + nameMiddles[Random.Range(0, nameMiddles.Length)] + nameEndings[Random.Range(0, nameEndings.Length)];
        largeStoneName = nameStarts[Random.Range(0, nameStarts.Length)] + nameMiddles[Random.Range(0, nameMiddles.Length)] + nameEndings[Random.Range(0, nameEndings.Length)];
    }

    void Spawn()
    {
        GameObject newStone = Instantiate(stones[Random.Range(0, stones.Length)], GetRandomSpawnPoint(), Quaternion.identity);
    }

    Vector2 GetRandomSpawnPoint()
    {
        return new Vector2(transform.position.x + Random.Range(-1,1), transform.position.y + Random.Range(-1,1)).normalized * 5;
    }

    void OnNextIsland()
    {
        RandomizeNames();
        stoneHpMultiplier *= 2;
        stoneBountyMultiplier *= 2;
    }
}
