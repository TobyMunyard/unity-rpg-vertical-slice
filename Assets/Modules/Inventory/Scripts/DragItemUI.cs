using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI element used to represent a inventory item being dragged around the inventory.
/// </summary>
public class DragItemUI : MonoBehaviour
{
    public static DragItemUI Instance { get; private set; }

    [SerializeField] private Image iconImage;
    [SerializeField] private CanvasGroup canvasGroup;
    private InventorySlotUI originSlot;

    /// <summary>
    /// Initializes the instance and hides itself from view.
    /// </summary>
    private void Awake()
    {
        Instance = this;
        Hide();
    }

    /// <summary>
    /// Called when a drag begins, sets the origin slot and makes the UI item 
    /// appear as the same icon as the dragged item.
    /// </summary>
    public void BeginDrag(Sprite icon, InventorySlotUI origin)
    {
        originSlot = origin;
        iconImage.sprite = icon;
        canvasGroup.alpha = 1f;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Drags the element to the new position each frame.
    /// </summary>
    public void Drag(Vector2 position)
    {
        transform.position = position;
    }

    /// <summary>
    /// Hides the element upon the drag ending.
    /// </summary>
    public void EndDrag()
    {
        Hide();
    }

    /// <summary>
    /// Makes the element transparent and turns it off.
    /// </summary>
    private void Hide()
    {
        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Called when a drag begins, sets the origin slot and makes the UI item 
    /// appear as the same icon as the dragged item.
    /// </summary>
    public InventorySlotUI GetOriginSlot()
    {
        return originSlot;
    }
}
