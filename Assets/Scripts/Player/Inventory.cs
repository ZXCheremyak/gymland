using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<InventoryItem> items;
    [SerializeField] Transform itemContainer;
    [SerializeField] GameObject itemPrefab;

    [SerializeField] Transform selectedItemDetails;
    [SerializeField] TextMeshProUGUI selectedItemName;
    [SerializeField] Image selectedItemIcon;
    [SerializeField] TextMeshProUGUI selectedItemBonus;
    [SerializeField] TextMeshProUGUI selectedItemRarity;
    [SerializeField] Button equipButton;
    [SerializeField] Button deleteButton;
    [SerializeField] Button combineButton;
    [SerializeField] Button equipBestButton;
    [SerializeField] Button deleteSelectedButton;
    [SerializeField] TextMeshProUGUI combineRequirements;

    private InventoryItem selectedItem;
    private List<InventoryItem> selectedItemsForDeletion = new List<InventoryItem>();
    void Start()
    {
        items = new List<InventoryItem>();
        
        items.Add(new InventoryItem("Panda", null, 3.5f, ItemType.Legendary));
        items.Add(new InventoryItem("Dino", null, 2.0f, ItemType.Rare));
        items.Add(new InventoryItem("Crow", null, 1.5f, ItemType.Common));

        RefreshUI();
    }

    public void AddItem(InventoryItem newItem)
    {
        items.Add(newItem);
        RefreshUI();
    }

    public void RefreshUI()
    {
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in items)
        {
            GameObject itemObject = Instantiate(itemPrefab, itemContainer);
            itemObject.GetComponent<ItemUI>().Setup(item, this);
        }
    }

    public void ShowItemDetails(InventoryItem item)
    {
        selectedItemName.text = item.Name;
        selectedItemIcon.sprite = item.Icon;
        selectedItemBonus.text = item.Bonus.ToString();
        selectedItemRarity.text = item.Rarity.ToString();

        equipButton.onClick.RemoveAllListeners();
        equipButton.onClick.AddListener(() => EquipItem(item));
        deleteButton.onClick.RemoveAllListeners();
        deleteButton.onClick.AddListener(() => DeleteItem(item));
        combineButton.onClick.RemoveAllListeners();
        combineButton.onClick.AddListener(() => CombineItem(item));
        UpdateCombineRequirements(item);
    }

    void UpdateCombineRequirements(InventoryItem item)
    {
        int count = items.FindAll(i => i.Name == item.Name).Count;
        combineRequirements.text = $"{count}/3";
    }

    public void EquipItem(InventoryItem item)
    {
        item.IsEquipped = !item.IsEquipped;
        RefreshUI();
    }

    public void DeleteItem(InventoryItem item)
    {
        items.Remove(item);
        RefreshUI();
    }

    public void CombineItem(InventoryItem item)
    {
        if (selectedItem != null)
        {
            List<InventoryItem> sameItems = items.FindAll(i => i.Name == selectedItem.Name);
            if (sameItems.Count >= 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    items.Remove(sameItems[i]);
                }

                InventoryItem upgradedItem = new InventoryItem(selectedItem.Name, selectedItem.Icon, selectedItem.Bonus * 2, selectedItem.Rarity);
                items.Add(upgradedItem);
                RefreshUI();
                ShowItemDetails(upgradedItem);
            }
        }
    }

    public void EquipBestItems()
    {
        var bestItems = items.OrderByDescending(item => item.Bonus).Take(3).ToList();

        foreach (var item in items)
        {
            item.IsEquipped = false;
        }

        foreach (var item in bestItems)
        {
            item.IsEquipped = true;
        }

        RefreshUI();
    }

    public void DeleteSelectedItems()
    {
        foreach (var item in selectedItemsForDeletion)
        {
            items.Remove(item);
        }

        selectedItemsForDeletion.Clear();
        RefreshUI();
    }

    public void SelectItemForDeletion(InventoryItem item)
    {
        if (selectedItemsForDeletion.Contains(item))
        {
            selectedItemsForDeletion.Remove(item);
        }
        else
        {
            selectedItemsForDeletion.Add(item);
        }
    }

    public void ShowGuide()
    {

    }
}
