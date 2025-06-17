using UnityEngine;

/// <summary>
/// Wrapper class for representing InventoryItem objects. Stores item and its quanitity.
/// </summary>
public class InventorySlot
{
    [Tooltip("Underlying InventoryItem used for internal representation.")]
    public InventoryItem item;
    [Tooltip("Quantity of items.")]
    public int quantity;

    /// <summary>
    /// Checks if the InventorySlot is empty (does not contain an InventoryItem). 
    /// Returns true if so, false otherwise.
    /// </summary>
    public bool isSlotEmpty()
    {
        if (item == null || quantity == 0) return true;

        return false;
    }

    /// <summary>
    /// Handles the addition of item(s) to the InventorySlot. Returns the number of leftover 
    /// items, if there are any.
    /// </summary>
    public int AddItem(InventoryItem item, int quantity)
    {
        if (isSlotEmpty())
        {
            this.item = item;
            int added = Mathf.Min(quantity, item.maxStackSize);
            this.quantity = added;
            return quantity - added; // leftover
        }
        else if (this.item.itemID == item.itemID)
        {
            int spaceLeft = item.maxStackSize - this.quantity;
            int added = Mathf.Min(spaceLeft, quantity);
            this.quantity += added;
            return quantity - added; // leftover
        }
        else
        {
            // Can't add different item here, all quantity left over
            return quantity;
        }
    }
}
