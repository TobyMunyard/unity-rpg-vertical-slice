using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Inventory Item")]
public class InventoryItem : ScriptableObject
{
    public string itemID;

    public string displayName;

    [TextArea]
    public string description;

    public Sprite icon;

    public bool isStackable = false;

    public int maxStackSize = 1;

}
