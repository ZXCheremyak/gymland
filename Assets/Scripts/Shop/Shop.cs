using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject shopMenu;

    [SerializeField]
    public InventoryItem[] items;
    public InventoryItem[] rareItems;
    public InventoryItem[] epicItems;
    public InventoryItem[] legendaryItems;

    [SerializeField] GameObject commonText;
    [SerializeField] GameObject rareText;
    [SerializeField] GameObject epicText;
    [SerializeField] GameObject legendaryText;

    [SerializeField] TextMeshProUGUI priceText;

    [SerializeField] int rollCost;

    void Start()
    {
        SetSprites(items, commonText);
        SetSprites(rareItems, rareText);
        SetSprites(epicItems, epicText);
        SetSprites(legendaryItems, legendaryText);

        shopMenu.GetComponent<Canvas>().worldCamera = Camera.main;

        priceText.text = "Price:  " + rollCost.ToString() + "$";
    }

    void SetSprites(InventoryItem[] items, GameObject imageParent)
    {
        for(int i = 0; i < items.Length; i++)
        {
            imageParent.transform.GetChild(i).GetComponent<Image>().sprite = items[i].Icon;
        }
    }

    public void BuyItem()
    {
        if (Parameters.money < rollCost) return;
        Parameters.money -= rollCost;

        EventManager.moneyChanged.Invoke();
        Inventory.instance.AddItem(GetRandomItem());
    }



    InventoryItem GetRandomItem()
    {
        InventoryItem item = null;
        int randomValue = Random.Range(0, 100);
        if(randomValue < 60)
        {
            item = items[Random.Range(0, items.Length)];
        }
        if(randomValue >= 60 && randomValue < 90)
        {
            item = rareItems[Random.Range(0, rareItems.Length)];
        }
        if(randomValue >= 90 && randomValue < 99)
        {
            item = epicItems[Random.Range(0, epicItems.Length)];
        }
        if(randomValue == 99)
        {
            item = legendaryItems[Random.Range(0, legendaryItems.Length)];
        }

        

        return new InventoryItem(item.Id, item.Name, item.Icon, item.Bonus, item.Rarity, item.GradeLevel);
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
