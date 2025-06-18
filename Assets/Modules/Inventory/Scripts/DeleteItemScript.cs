using UnityEngine;
using UnityEngine.EventSystems;

public class DeleteItemScript : MonoBehaviour, IDropHandler
{

    private Inventory inventory;

    /// <summary>
    /// Gets inventory from the singleton InventoryUI instance.
    /// </summary>
    private void Awake()
    {
        inventory = InventoryUI.Instance.GetInventory();
    }

    /// <summary>
    /// Called once a drag is released on the trash can, deletes the item by creating a new empty slot in its place.
    /// </summary>
    public void OnDrop(PointerEventData eventData)
    {
        var originSlot = DragItemUI.Instance.GetOriginSlot();
        if (originSlot == null || originSlot == this) return;

        inventory.GetInventorySlots()[originSlot.slotIndex] = new InventorySlot();
        InventoryUI.Instance.Refresh(inventory);
    }
}
