using UnityEngine;
using System.Collections.Generic;

public class PlayerInventoryManager : MonoBehaviour
{
    public static PlayerInventoryManager Instance { get; private set; }

    private Dictionary<string, InventoryItem> itemLookup;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        LoadAllItems();
    }

    private void LoadAllItems()
    {
        itemLookup = new Dictionary<string, InventoryItem>();
        InventoryItem[] items = Resources.LoadAll<InventoryItem>("Items");

        foreach (var item in items)
        {
            if (!itemLookup.ContainsKey(item.itemID))
                itemLookup.Add(item.itemID, item);
        }
    }

    public InventoryItem GetItemByID(string id)
    {
        return itemLookup.TryGetValue(id, out var item) ? item : null;
    }
}
