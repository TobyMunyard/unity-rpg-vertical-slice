using UnityEngine;

/// <summary>
/// Underlying representation of items within a player's inventory. Can be created from the asset menu.
/// </summary>
[CreateAssetMenu(menuName = "Inventory/Inventory Item")]
public class InventoryItem : ScriptableObject
{
    [Tooltip("The underlying ID of the item, not shown to players.")]
    public string itemID;
    [Tooltip("The name of the item, will be shown to players.")]
    public string displayName;
    [Tooltip("The description of the item, will be shown to players.")]
    [TextArea]
    public string description;
    [Tooltip("The icon representing the item in the UI, will be shown to players.")]
    public Sprite icon;
    [Tooltip("Can the item have more than one in a slot?")]
    public bool isStackable = false;
    [Tooltip("Maximum amount of the item that can be placed in one slot")]
    public int maxStackSize = 1;

}
