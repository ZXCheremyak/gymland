using UnityEngine;

public enum ItemType { Common, Rare, Epic, Legendary }

[System.Serializable]
public class InventoryItem
{
    public int Id;
    public string Name;
    public Sprite Icon;
    public float Bonus;
    public ItemType Rarity;
    public int GradeLevel;
    public bool IsEquipped;

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