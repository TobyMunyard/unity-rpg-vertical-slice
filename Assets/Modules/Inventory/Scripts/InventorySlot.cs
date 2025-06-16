using UnityEngine;

public class InventorySlot
{
    public InventoryItem item;

    public int quantity;

    public bool isSlotEmpty()
    {
        if (item == null || quantity == 0) return true;

        return false;
    }

    public bool addItem(InventoryItem item)
    {
        if (isSlotEmpty())
        {
            this.item = item;
            quantity++;
            return true;
        }
        else if (this.item.itemID == item.itemID && (quantity + 1 <= item.maxStackSize))
        {
            this.item = item;
            quantity++;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool addItem(InventoryItem item, int quantity)
    {
        if (isSlotEmpty())
        {
            this.item = item;
            this.quantity = Mathf.Min(quantity, item.maxStackSize);
            return true;
        }

        if (this.item.itemID == item.itemID && this.quantity < item.maxStackSize)
        {
            int spaceLeft = item.maxStackSize - this.quantity;
            this.quantity += Mathf.Min(spaceLeft, quantity);
            return true;
        }

        return false;
    }
}
