using UnityEngine;

/// <summary>
/// UI representation of underlying player inventory. Populates a grid layout with 
/// all inventory items.
/// </summary>
public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }

    [SerializeField] private Transform gridParent;
    [SerializeField] private InventorySlotUI slotPrefab;
    private Inventory inventory;

    // Cache slot UIs so we don't destroy/create every refresh
    private InventorySlotUI[] slotUIs;

    /// <summary>
    /// Enforces singleton structure, creating only a single instance of this class.
    /// </summary>
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /// <summary>
    /// Sets the active inventory so this UI can operate on it.
    /// </summary>
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        // Instantiate slotUIs once to match inventory size
        if (slotUIs == null || slotUIs.Length != inventory.size)
        {
            // Clear old
            foreach (Transform child in gridParent)
                Destroy(child.gameObject);

            slotUIs = new InventorySlotUI[inventory.size];
            for (int i = 0; i < inventory.size; i++)
            {
                var slotUI = Instantiate(slotPrefab, gridParent, false);
                slotUI.inventoryUI = this;
                slotUIs[i] = slotUI;
            }
        }

        RefreshAllSlots();
    }

    /// <summary>
    /// Loops through all the slots and refreshes them, ensuring up to date UI information.
    /// </summary>
    public void RefreshAllSlots()
    {
        for (int i = 0; i < inventory.size; i++)
        {
            slotUIs[i].SetSlot(inventory.slots[i], i);
        }
    }

    /// <summary>
    /// Refreshes the UI when called, ensures display of items is up to date.
    /// </summary>
    public void Refresh(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshAllSlots();
    }

    /// <summary>
    /// Called when a drag-and-drop operation requests an item move.
    /// </summary>
    public void OnDropRequest(int fromIndex, int toIndex)
    {
        bool moved = inventory.MoveItems(fromIndex, toIndex);
        if (moved)
            RefreshAllSlots();
    }

    public Inventory GetInventory() { return inventory; }
}
