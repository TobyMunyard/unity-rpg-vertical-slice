using UnityEngine;

/// <summary>
/// Handles underlying representation of player inventory and makes calls to add and remove items.
/// </summary>
public class Inventory
{
    [Tooltip("Underlying representation of Inventory, broken up into slots.")]
    public InventorySlot[] slots;
    [Tooltip("Number of total slots in the inventory.")]
    public int size => slots.Length;

    /// <summary>
    /// Initializes the player inventory with size slots.
    /// </summary>
    public Inventory(int size) {
        slots = new InventorySlot[size];
        for (int i = 0; i < size; i++)
        {
            slots[i] = new InventorySlot();
        }
    }

    /// <summary>
    /// Trys to add quantity amount of item.
    /// </summary>
    public bool TryAddItem(InventoryItem item, int quantity)
    {
        int remaining = quantity;

        // Try stacking into an existing slot
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null && slots[i].item.itemID == item.itemID)
            {
                remaining = slots[i].AddItem(item, remaining);
                if (remaining <= 0)
                    return true; // all added
            }
        }

        // Try placing into an empty slot
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].isSlotEmpty())
            {
                remaining = slots[i].AddItem(item, remaining);
                if (remaining <= 0)
                    return true; // all added
            }
        }

        // If we get here, inventory is full or no space for all items
        return remaining < quantity; // true if at least some added
    }

    /// <summary>
    /// Gets all the inventory slots in the player inventory.
    /// </summary>
    public InventorySlot[] GetInventorySlots() => slots;
}
