using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] Image icon;
    private InventoryItem item;
    private Inventory inventoryManager;
    private bool isSelectedForDeletion;

    [SerializeField] GameObject selectionFrame;
    [SerializeField] GameObject equippedMarker;
    [SerializeField] GameObject firstStar;
    [SerializeField] GameObject secondStar;
    [SerializeField] GameObject thirdStar;

    public void Setup(InventoryItem newItem, Inventory manager)
    {
        item = newItem;
        inventoryManager = manager;

        icon.sprite = item.Icon;
        equippedMarker.SetActive(newItem.IsEquipped);

        switch(item.GradeLevel)
        {
            case(1):
                firstStar.SetActive(true);
                break;
            case(2):
                firstStar.SetActive(true);
                secondStar.SetActive(true);
                break;
            case(3):
                firstStar.SetActive(true);
                secondStar.SetActive(true);
                thirdStar.SetActive(true);
                break;
        }

        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(() => OnItemClick(item));
    }
    private void OnItemClick(InventoryItem _item)
    {
            inventoryManager.ShowItemDetails(_item, GetComponent<ItemUI>());
    }
    public void SetSelected(bool isSelected)
    {
        selectionFrame.SetActive(isSelected);
    }

    public void SetEquiped(bool isEquiped)
    {
        equippedMarker.SetActive(isEquiped);
    }
}