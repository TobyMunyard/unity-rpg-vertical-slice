[System.Serializable]
public class InventorySlotData
{
    public string itemID;
    public int quantity;

    public InventorySlotData(string itemID, int quantity)
    {
        this.itemID = itemID;
        this.quantity = quantity;
    }
}
