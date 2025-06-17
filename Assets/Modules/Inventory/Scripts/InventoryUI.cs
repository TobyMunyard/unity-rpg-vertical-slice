using UnityEngine;

/// <summary>
/// UI representation of underlying player inventory. Populates a grid layout with 
/// all inventory items.
/// </summary>
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform gridParent;
    [SerializeField] private InventorySlotUI slotPrefab;

    /// <summary>
    /// Refreshes the UI when called, ensures display of items is up to date.
    /// </summary>
    public void Refresh(Inventory inventory)
    {
        // Clears out old slots so data is up to date
        foreach (Transform child in gridParent)
            Destroy(child.gameObject);

        // Add new slots
        foreach (InventorySlot slot in inventory.GetInventorySlots())
        {
            var ui = Instantiate(slotPrefab, gridParent, false);
            ui.SetSlot(slot);
        }
    }
}
