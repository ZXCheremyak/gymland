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

    public void Setup(InventoryItem newItem, Inventory manager)
    {
        item = newItem;
        inventoryManager = manager;
        icon.sprite = item.Icon;
        GetComponent<Button>().onClick.AddListener(() => OnItemClick());
    }
    private void OnItemClick()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            ToggleSelectionForDeletion();
        }
        else
        {
            inventoryManager.ShowItemDetails(item);
        }
    }

    private void ToggleSelectionForDeletion()
    {
        isSelectedForDeletion = !isSelectedForDeletion;
        GetComponent<Image>().color = isSelectedForDeletion ? Color.red : Color.white;
        inventoryManager.SelectItemForDeletion(item);
    }
}
