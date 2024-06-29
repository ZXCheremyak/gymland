using UnityEngine;

public enum ItemType { Common, Rare, Epic, Legendary }
public class InventoryItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Sprite Icon { get; set; }
    public float Bonus { get; set; }
    public ItemType Rarity { get; set; }
    public int GradeLevel { get; set; }
    public bool IsEquipped { get; set; }

    public InventoryItem(int id, string name, Sprite icon, float bonus, ItemType rarity, int gradeLevel)
    {
        Id = id;
        Name = name;
        Icon = icon;
        Bonus = bonus;
        Rarity = rarity;
        GradeLevel = gradeLevel;
        IsEquipped = false;
    }
}

