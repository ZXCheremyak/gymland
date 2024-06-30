using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private List<InventoryItem> items;
    [SerializeField] Transform itemContainer;
    [SerializeField] GameObject itemPrefab;

    [SerializeField] GameObject selectedItemDetails;
    [SerializeField] GameObject helpPanel;
    [SerializeField] GameObject newItemPanel;
    [SerializeField] TextMeshProUGUI selectedItemName;
    [SerializeField] Image selectedItemIcon;
    [SerializeField] TextMeshProUGUI selectedItemBonus;
    [SerializeField] TextMeshProUGUI selectedItemRarity;
    [SerializeField] TextMeshProUGUI newItemName;
    [SerializeField] TextMeshProUGUI equippedCountText;
    [SerializeField] TextMeshProUGUI multiplierText;
    [SerializeField] Image newItemIcon;
    [SerializeField] TextMeshProUGUI newItemBonus;
    [SerializeField] TextMeshProUGUI newItemRarity;
    [SerializeField] Button equipButton;
    [SerializeField] Button deleteButton;
    [SerializeField] Button combineButton;
    [SerializeField] Button equipBestButton;
    [SerializeField] TextMeshProUGUI combineRequirements;

    private InventoryItem selectedItem = null;
    private ItemUI selectedItemUI = null;

    private int equippedCount = 0;
    private bool helpIsOpened = false;
    private Canvas inventroyCanvas;
    private List<InventoryItem> selectedItemsForDeletion = new List<InventoryItem>();

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        items = new List<InventoryItem>();

        inventroyCanvas = this.gameObject.GetComponent<Canvas>();
        inventroyCanvas.enabled = false;

        ClearItemDetails(); 

        RefreshUI();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        inventroyCanvas.enabled = !inventroyCanvas.enabled;
    }

    public void AddItem(InventoryItem newItem)
    {
        newItemPanel.SetActive(true);
        newItemName.text = newItem.Name;
        newItemIcon.sprite = newItem.Icon;
        newItemBonus.text = newItem.Bonus.ToString() + "x";

        newItemRarity.text = newItem.Rarity.ToString();

        switch(newItem.Rarity)
        {
            case(ItemType.Common):
                newItemRarity.color = Color.gray;
                break;
            case(ItemType.Rare):
                newItemRarity.color = Color.cyan;
                break;
            case(ItemType.Epic):
                newItemRarity.color = Color.magenta;
                break;
            case(ItemType.Legendary):
                newItemRarity.color = Color.red;
                break;
        }

        items.Add(newItem);
        RefreshUI();
    }

    public void RefreshUI()
    {
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }

        items.Sort((x,y) => y.Bonus.CompareTo(x.Bonus));

        foreach (var item in items)
        {
            GameObject itemObject = Instantiate(itemPrefab, itemContainer);
            itemObject.GetComponent<ItemUI>().Setup(item, this);
        }

        equippedCountText.text = "Equiped:  " + equippedCount + "  /  3";
        multiplierText.text = "Multiplier:  " + Parameters.powerGrowthMultiplier + "x";
    }

    public void ShowItemDetails(InventoryItem item, ItemUI itemUI)
    {
        if(selectedItem != null)
        {
            selectedItemUI.SetSelected(false);
        }

        selectedItem = item;
        selectedItemUI = itemUI;
        selectedItemUI.SetSelected(true);

        if(item.GradeLevel == 0) selectedItemName.text = item.Name;
        else selectedItemName.text = item.GradeLevel.ToString() + " star  " + item.Name;

        selectedItemIcon.sprite = item.Icon;
        selectedItemBonus.text = item.Bonus.ToString() + "x";
        selectedItemRarity.text = item.Rarity.ToString();

        switch(item.Rarity)
        {
            case(ItemType.Common):
                selectedItemRarity.color = Color.gray;
                break;
            case(ItemType.Rare):
                selectedItemRarity.color = Color.cyan;
                break;
            case(ItemType.Epic):
                selectedItemRarity.color = Color.magenta;
                break;
            case(ItemType.Legendary):
                selectedItemRarity.color = Color.red;
                break;
        }

        if(item.IsEquipped)
        {
            equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Unequip";
        }
        else
        {
            equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
        }

        equipButton.onClick.RemoveAllListeners();
        equipButton.onClick.AddListener(() => EquipItem(item, itemUI));
        deleteButton.onClick.RemoveAllListeners();
        deleteButton.onClick.AddListener(() => DeleteItem(item));
        combineButton.onClick.RemoveAllListeners();
        combineButton.onClick.AddListener(() => CombineItem(item, itemUI));
        UpdateCombineRequirements(item);

        selectedItemDetails.SetActive(true);

        equippedCountText.text = "Equiped:   " + equippedCount + "  /  3";
        multiplierText.text = "Multiplier:  " + Parameters.powerGrowthMultiplier + "x";
    }

    void UpdateCombineRequirements(InventoryItem item)
    {
        int count = items.FindAll(i => i.Id == item.Id).Count;
        combineRequirements.text = $"{count}/3";
    }

    public void EquipItem(InventoryItem item, ItemUI itemUI)
    {
        if (item.IsEquipped)
        {
            equippedCount--;
            Parameters.powerGrowthMultiplier -= item.Bonus;
        }
        else if (!item.IsEquipped && equippedCount < 3) 
        {
            equippedCount++;
            Parameters.powerGrowthMultiplier += item.Bonus;
        }
        else return;

            item.IsEquipped = !item.IsEquipped;
            
            itemUI.SetEquiped(item.IsEquipped);
            ShowItemDetails(item, itemUI);
            
            selectedItem = item;
            selectedItemUI = itemUI;

            equippedCountText.text = "Equiped:   " + equippedCount + "  /  3";
            multiplierText.text = "Multiplier:  " + Parameters.powerGrowthMultiplier + "x";
    }

    public void DeleteItem(InventoryItem item)
    {
        items.Remove(item);
        if (item.IsEquipped)
        {
            equippedCount--;
            Parameters.powerGrowthMultiplier -= item.Bonus;
        }
        RefreshUI();
        ClearItemDetails();

        selectedItem = null;
    }

    public void CombineItem(InventoryItem item, ItemUI itemUI)
    {
        if (selectedItem != null)
        {
            List<InventoryItem> sameItems = items.FindAll(i => i.Id == selectedItem.Id);

            if (sameItems.Count >= 3 && selectedItem.GradeLevel < 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    items.Remove(sameItems[i]);
                    if (sameItems[i].IsEquipped)
                    {
                        equippedCount--;
                        Parameters.powerGrowthMultiplier -= sameItems[i].Bonus;
                    }
                }

                InventoryItem upgradedItem = new InventoryItem(selectedItem.Id * 10, selectedItem.Name, selectedItem.Icon, selectedItem.Bonus * 2, selectedItem.Rarity, selectedItem.GradeLevel + 1);
                AddItem(upgradedItem);
                RefreshUI();

                selectedItem = null;
                selectedItemUI = null;
                ClearItemDetails();
            }
        }
    }

    public void EquipBestItems()
    {
        var bestItems = items.OrderByDescending(item => item.Bonus).Take(3).ToList();

        foreach (var item in items)
        {
            item.IsEquipped = false;
            Parameters.powerGrowthMultiplier = 1f;
        }

        foreach (var item in bestItems)
        {
            item.IsEquipped = true;
            Parameters.powerGrowthMultiplier += item.Bonus;
        }
        equippedCount = bestItems.Count();

        RefreshUI();

        selectedItem = null;
        selectedItemUI = null;

        ClearItemDetails();

    }

    void ClearItemDetails()
    {
        selectedItemDetails.SetActive(false);
    }

    public void ShowGuide()
    {
        helpIsOpened = !helpIsOpened;
        helpPanel.SetActive(helpIsOpened);
    }

    public void CloseNewItemPanel()
    {
        newItemPanel.SetActive(false);
    }
}