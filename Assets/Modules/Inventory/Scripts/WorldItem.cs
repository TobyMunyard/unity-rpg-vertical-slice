using UnityEngine;

/// <summary>
/// Attached to WorldItem prefabs to display items physically in the game.
/// </summary>
public class WorldItem : MonoBehaviour 
{
    [Tooltip("Underlying InventoryItem used for internal representation.")]
    public InventoryItem item;
    [Tooltip("Number of items that are in this WorldItem, default 1.")]
    public int quantity = 1;
}
