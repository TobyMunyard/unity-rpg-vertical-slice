using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform gridParent;
    [SerializeField] private InventorySlotUI slotPrefab;

    public void Refresh(Inventory inventory)
    {
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
