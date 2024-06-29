using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{

    [SerializeField] int hp, bounty;

    void Start()
    {

    }

    void TakeDamage(int damage)
    {
        hp -= damage;
    }

    void Die()
    {
        EventManager.moneyChanged.Invoke(bounty);
        EventManager.stoneDestroyed.Invoke();
        Destroy(gameObject);
    }
}
