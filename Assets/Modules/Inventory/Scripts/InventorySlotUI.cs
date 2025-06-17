using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// UI wrapper for InventorySlot objects. If a slot is empty it will show an 
/// empty slot icon, otherwise it will show the icon of the item in the slot.
/// </summary>
public class InventorySlotUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
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
    private InventorySlot slotData;
    public InventoryUI inventoryUI;
    public int slotIndex;

    /// <summary>
    /// Sets the content of the item slot in the UI to match the underlying slot.
    /// </summary>
    public void SetSlot(InventorySlot slot, int index)
    {
        slotData = slot;
        slotIndex = index;

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

    /// <summary>
    /// Begins a dragging operation for a inventory UI element.
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slotData.isSlotEmpty()) return;
        DragItemUI.Instance.BeginDrag(slotData.item.icon, this);
    }

    /// <summary>
    /// Called as long as the drag is still occuring, continuing to move the UI element.
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        DragItemUI.Instance.Drag(eventData.position);
    }

    /// <summary>
    /// Called once a drag ends.
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        DragItemUI.Instance.EndDrag();
    }

    /// <summary>
    /// Called on click.
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        // Not in use yet but could be used later.
    }

    /// <summary>
    /// Called once a drag is released.
    /// </summary>
    public void OnDrop(PointerEventData eventData)
    {
        var originSlot = DragItemUI.Instance.GetOriginSlot();
        if (originSlot == null || originSlot == this) return;

        inventoryUI.OnDropRequest(originSlot.slotIndex, this.slotIndex);
    }
}
