using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI wrapper for InventorySlot objects. If a slot is empty it will show an 
/// empty slot icon, otherwise it will show the icon of the item in the slot.
/// </summary>
public class InventorySlotUI : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The image of an item to be displayed on the UI.")]
    private Image iconImage;
    [SerializeField]
    [Tooltip("The item quantity in the slot, shown in UI.")]
    private TextMeshProUGUI quantityText;
    [SerializeField]
    [Tooltip("Represents an empty slot in the inventory.")]
    private Sprite emptySlotIcon;

    /// <summary>
    /// Sets the content of the item slot in the UI to match the underlying slot.
    /// </summary>
    public void SetSlot(InventorySlot slot)
    {
        if (slot.isSlotEmpty())
        {
            // Slot doesn't have an item, show it as empty
            iconImage.sprite = emptySlotIcon;
            quantityText.text = "";
        }
        else
        {
            // Slot has an item in it, display it for the player
            iconImage.enabled = true;
            iconImage.sprite = slot.item.icon;
            quantityText.text = slot.quantity > 1 ? slot.quantity.ToString() : "";
        }
    }
}
