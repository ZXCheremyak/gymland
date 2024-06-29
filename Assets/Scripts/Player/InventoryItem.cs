using UnityEngine;

public enum ItemType { Common, Rare, Epic, Legendary }
public class InventoryItem
{
    public string Name { get; set; }
    public Sprite Icon { get; set; }
    public float Bonus { get; set; }
    public ItemType Rarity { get; set; }
    public bool IsEquipped { get; set; }

    public InventoryItem(string name, Sprite icon, float bonus, ItemType rarity)
    {
        Name = name;
        Icon = icon;
        Bonus = bonus;
        Rarity = rarity;
        IsEquipped = false;
    }
}

