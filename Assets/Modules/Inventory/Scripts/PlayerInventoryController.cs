using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Oversees the interactions with Inventory and ensures UI stays in line with underlying representation.
/// </summary>
public class PlayerInventoryController : MonoBehaviour
{
    public InventoryUI inventoryUI;

    private Inventory inventory;

    [SerializeField]
    public InventoryItem testItem;

    [SerializeField]
    public InventoryItem secondTestItem;

    [SerializeField]
    private GameObject inventoryUIRoot;

    /// <summary>
    /// Initializes the underlying inventory, add any test items here.
    /// </summary>
    private void Start()
    {
        inventory = new Inventory(20); // CHANGE INVENTORY SIZE HERE
        inventoryUI.SetInventory(inventory);
        inventoryUI.Refresh(inventory);

        // TEMP: Add test item
        for (int i = 0; i < 10; i++)
        {
            inventory.TryAddItem(testItem, 1);
        }
        inventoryUI.Refresh(inventory);

        inventory.TryAddItem(secondTestItem, 2);
        inventoryUI.Refresh(inventory);
    }

    /// <summary>
    /// Decoupled method for adding items to the underlying inventory representation.
    /// </summary>
    public void AddItem(InventoryItem item, int quantity)
    {
        if (inventory.TryAddItem(item, quantity))
        {
            inventoryUI.Refresh(inventory);
        }
    }

    /// <summary>
    /// Gets the underlying Inventory class.
    /// </summary>
    public Inventory GetInventory() => inventory;

    /// <summary>
    /// Checks each frame if the player is trying to open or close their inventory.
    /// </summary>
    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            inventoryUIRoot.SetActive(!inventoryUIRoot.activeSelf);
        }
    }
}
