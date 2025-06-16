using UnityEngine;
using UnityEngine.InputSystem;

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

    private void Start()
    {
        inventory = new Inventory(20); // CHANGE INVENTORY SIZE HERE
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

    public void AddItem(InventoryItem item, int quantity)
    {
        if (inventory.TryAddItem(item, quantity))
        {
            inventoryUI.Refresh(inventory);
        }
    }

    public Inventory GetInventory() => inventory;

    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            inventoryUIRoot.SetActive(!inventoryUIRoot.activeSelf);
        }
    }
}
