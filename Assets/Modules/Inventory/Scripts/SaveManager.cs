using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private List<InventorySlotData> inventorySlots = new List<InventorySlotData>();
    private Inventory inventory;

    private static int currentSave = 1;

    private IEnumerator Start()
    {
        // Waits to ensure InventoryUI is not null
        while (InventoryUI.Instance == null)
            yield return null;

        inventory = InventoryUI.Instance.GetInventory();

        currentSave = PlayerPrefs.GetInt("CurrentSave", 1);
    }

    public void Save()
    {
        inventorySlots = new List<InventorySlotData>();
        // Add each inventoryItem to a list
        for (int i = 0; i < inventory.size; i++)
        {
            var slot = inventory.GetInventorySlots()[i];
            if (!slot.isSlotEmpty())
            {
                string itemID = slot.item.itemID;
                int itemQuantity = slot.quantity;
                inventorySlots.Add(new InventorySlotData(itemID, itemQuantity));
            }
            else
            {
                // Save an empty slot
                inventorySlots.Add(new InventorySlotData(null, 0));
            }
        }
        // Save the list to a serialisable object
        InventorySaveData saveData = new InventorySaveData();
        saveData.slots = inventorySlots;

        // Create a Saves directory for storing saves if it doesn't exist
        string folderPath = Path.Combine(Application.persistentDataPath, "Saves");
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        // Create a dynamic filename based on the number of the save
        string fileName = $"saveFile_{currentSave}.json";
        File.WriteAllText(Path.Combine(folderPath, fileName), JsonUtility.ToJson(saveData));

        // Increment the save counter
        currentSave++;

        // Keep track of the current save in PlayerPrefs
        PlayerPrefs.SetInt("CurrentSave", currentSave);
        PlayerPrefs.Save();

        InventoryUI.Instance.Refresh(inventory);
    }

    public void Load(int saveSlot)
    {
        // Reset the inventory to prevent any duplication
        inventory = new Inventory(inventory.size);
        InventoryUI.Instance.SetInventory(inventory);
        InventoryUI.Instance.Refresh(inventory);

        // Create a Saves directory for storing saves if it doesn't exist
        string folderPath = Path.Combine(Application.persistentDataPath, "Saves");
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        try
        {
            // Read in the saved JSON file to get inventory data
            string json = File.ReadAllText(Path.Combine(folderPath, $"saveFile_{saveSlot}.json"));
            InventorySaveData saveData = JsonUtility.FromJson<InventorySaveData>(json);
            inventorySlots = saveData.slots;
            InventorySlotData[] saves = inventorySlots.ToArray();

            // Loop through each serialised item and reconstruct/retrieve its real InventoryItem 
            for (int i = 0; i < saves.Length; i++)
            {
                var savedSlot = saves[i];
                if (!string.IsNullOrEmpty(savedSlot.itemID))
                {
                    InventoryItem item = PlayerInventoryManager.Instance.GetItemByID(savedSlot.itemID);
                    if (item != null)
                    {
                        inventory.SetSlot(i, item, savedSlot.quantity);
                    }
                }
                else
                {
                    inventory.slots[i].item = null;
                    inventory.slots[i].quantity = 0;
                }
            }

        }
        catch (Exception e) // JSON parsing and IO errors
        {
            // Failed to load the file, log a error
            Debug.LogError($"Save file load failed: {e.Message}");
        }


        InventoryUI.Instance.SetInventory(inventory);
    }

}
