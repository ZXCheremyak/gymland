using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject shopMenu;

    [SerializeField]
    public InventoryItem[] items;
    public InventoryItem[] rareItems;
    public InventoryItem[] epicItems;
    public InventoryItem legendaryItem;

    [SerializeField] int rollCost;

    public void BuyItem()
    {
        Parameters.money -= rollCost;
    }

    InventoryItem GetRandomItem()
    {
        int randomValue = Random.Range(0, 100);
        if(randomValue < 60)
        {
            return items[Random.Range(0, items.Length)];
        }
        if(randomValue >= 60 && randomValue < 90)
        {
            return rareItems[Random.Range(0, rareItems.Length)];
        }
        if(randomValue >= 90 && randomValue < 99)
        {
            return epicItems[Random.Range(0, epicItems.Length)];
        }
        if(randomValue == 99)
        {
            return legendaryItem;
        }

        return null;
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.TryGetComponent<PlayerController>(out _))
        {
            shopMenu.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.TryGetComponent<PlayerController>(out _))
        {
            shopMenu.SetActive(false);
        }
    }
}
