
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("only gameplay")]
    new public string name = "Item";
    public string description;
    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(4, 5);
    public int count;
    
    [Header("only UI")]
    public bool stackable = true;


    [Header("both")]
    public Sprite icon = null;

    public Item(string name, string description, Sprite icon)
    {
        this.name = name;
        this.description = description;
        this.icon = icon;
    }

    public enum ItemType
    {
        CraftingItem,
        Tool,
        Machine,
        Sellable
    }
    public enum ActionType
    {
        Chop,
        Mine,
        Pickup
    }
}
