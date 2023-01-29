using UnityEngine;

[CreateAssetMenu(menuName = "Items/New Item")]
public class ItemData : ScriptableObject {

    [SerializeField] private string _id;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private float _price;
    [SerializeField] private float _weight;
    [SerializeField] private ItemType _itemType;

    [SerializeField] private Vector2Int _size;

    public string Id => _id;
    public string Name => _name;
    public Sprite Icon => _icon;
    public float Price => _price;
    public float Weight => _weight;
    public ItemType ItemType => _itemType;
    public Vector2Int Size => _size;

    public virtual void Use() {
        Inventory.instance.RemoveItem(this);
    }

    public virtual void Drop() {
        Inventory.instance.RemoveItem(this);
    }
}

public enum ItemType {
    Weapon,
    Armor,
    Helmet,
    Medical,
    Food,
    Artifact,
    Trash
}