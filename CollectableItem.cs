using UnityEngine;

public class CollectableItem : MonoBehaviour {
    public ItemData data;

    public void PickUp() {
        Inventory.instance.AddItem(data);
        Destroy(gameObject);
    }
}
