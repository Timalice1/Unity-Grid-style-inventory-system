using System;

[Serializable]
public class InventoryItem
{
    public ItemData data;
    public int stackSize;

    public InventoryItem(ItemData source) {
        data = source;
        AddToStack();
    }

    public void AddToStack() {
        stackSize++;
        Inventory.instance.inventoryWeight += data.Weight;
    }
    public void RemoveFromStack() {
        stackSize--;
        Inventory.instance.inventoryWeight -= data.Weight;
    }
}
