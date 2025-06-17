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
    public Inventory(int size)
    {
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

    /// <summary>
    /// Moves items within the inventory, uses a origin and destination index to appropriately move items.
    /// </summary>
    public bool MoveItems(int fromIndex, int toIndex)
    {
        if (fromIndex < 0 || fromIndex >= slots.Length ||
            toIndex < 0 || toIndex >= slots.Length ||
            fromIndex == toIndex)
        {
            return false; // invalid indices or same slot
        }

        InventorySlot fromSlot = slots[fromIndex];
        InventorySlot toSlot = slots[toIndex];

        if (fromSlot.isSlotEmpty())
        {
            return false; // nothing to move
        }

        // If destination is empty, move everything
        if (toSlot.isSlotEmpty())
        {
            toSlot.item = fromSlot.item;
            toSlot.quantity = fromSlot.quantity;

            fromSlot.item = null;
            fromSlot.quantity = 0;

            return true;
        }
        else if (toSlot.item.itemID == fromSlot.item.itemID)
        {
            // Same item type, stack as much as possible
            int spaceLeft = toSlot.item.maxStackSize - toSlot.quantity;
            if (spaceLeft <= 0)
                return false; // no space to add

            int moveAmount = Mathf.Min(spaceLeft, fromSlot.quantity);
            toSlot.quantity += moveAmount;
            fromSlot.quantity -= moveAmount;

            // Clear fromSlot if emptied
            if (fromSlot.quantity <= 0)
            {
                fromSlot.item = null;
                fromSlot.quantity = 0;
            }
            return true;
        }

        // Different item types, swap the two slots
        InventoryItem tempItem = toSlot.item;
        int tempQuantity = toSlot.quantity;

        toSlot.item = fromSlot.item;
        toSlot.quantity = fromSlot.quantity;

        fromSlot.item = tempItem;
        fromSlot.quantity = tempQuantity;

        return true;
    }

    /// <summary>
    /// Returns the inventory instance.
    /// </summary>
    public Inventory GetInventory()
    {
        return this;
    }

    /// <summary>
    /// Sets the item and quantity of the given slot/index.
    /// </summary>
    public void SetSlot(int index, InventoryItem item, int quantity)
    {
        if (index >= 0 && index < slots.Length)
        {
            slots[index].item = item;
            slots[index].quantity = quantity;
        }
    }


}
