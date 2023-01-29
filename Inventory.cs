using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Create a singletone
    public static Inventory instance;

    private Dictionary<ItemData, InventoryItem> itemsDictionary;
    public List<InventoryItem> inventory;

    //Inventory update event
    public delegate void InventoryUpdated();
    public event InventoryUpdated onInventoryUpdated;

    public float inventoryWeight;
    public float inventoryMaxWeight;

    private void Awake() {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        itemsDictionary = new Dictionary<ItemData, InventoryItem>();
        inventory = new List<InventoryItem>();

        inventoryMaxWeight = 50f;
    }

    public void AddItem(ItemData source) {
        if (itemsDictionary.TryGetValue(source, out InventoryItem value)) {
            value.AddToStack();
        }
        else {
            var item = new InventoryItem(source);
            itemsDictionary.Add(source, item);
            inventory.Add(item);
        }
        onInventoryUpdated?.Invoke();
    }

    public void RemoveItem(ItemData source) { 
        if(itemsDictionary.TryGetValue(source, out InventoryItem value)) {
            if(value.stackSize <= 1) {
                itemsDictionary.Remove(source);
                inventory.Remove(value);
            }
            else
                value.RemoveFromStack();
        }
        onInventoryUpdated?.Invoke();
    }
}