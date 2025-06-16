using UnityEngine;

public class Inventory
{
    public InventorySlot[] slots;

    public int size => slots.Length;

    public Inventory(int size) {
        slots = new InventorySlot[size];
        for (int i = 0; i < size; i++)
        {
            slots[i] = new InventorySlot();
        }
    }

    public bool TryAddItem(InventoryItem item, int quantity)
    {
        // Try stacking into an existing slot
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null && slots[i].item.itemID == item.itemID)
            {
                if (slots[i].addItem(item, quantity))
                    return true;
            }
        }

        // Try placing into an empty slot
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].isSlotEmpty())
            {
                if (slots[i].addItem(item, quantity))
                    return true;
            }
        }

        return false; // inventory full or stacking failed
    }

    public InventorySlot[] GetInventorySlots() => slots;
}
