using UnityEngine;

public enum ItemType { Common, Rare, Epic, Legendary }

[System.Serializable]
public class InventoryItem
{
    
    public string Name;
    public Sprite Icon;
    public float Bonus;
    public ItemType Rarity;
    public bool IsEquipped;
    
    public InventoryItem(string name, Sprite icon, float bonus, ItemType rarity)
    {
        Name = name;
        Icon = icon;
        Bonus = bonus;
        Rarity = rarity;
        IsEquipped = false;
    }
}