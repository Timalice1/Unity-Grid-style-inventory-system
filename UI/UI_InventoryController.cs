using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UI_InventoryController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Grid inventoryGrid;
    [SerializeField] private Text weightLabel;
    [SerializeField] private GameObject _slotPrefab;

    private void Start() {
        Inventory.instance.onInventoryUpdated += UpdateInventory;
    }

    private void UpdateInventory() {
        inventoryGrid.Clear();
        RedrawInventory();
        weightLabel.text = $"Weight: {Inventory.instance.inventoryWeight} KG (Max. {Inventory.instance.inventoryMaxWeight} KG)";
    }

    private void RedrawInventory() {
        foreach (var item in Inventory.instance.inventory) {
            //Instantiate new slot
            var slot = Instantiate(_slotPrefab).GetComponent<UI_Slot>();
            slot.Set(item);
            //Place slot on the grid
            inventoryGrid.PlaceSlot(slot);
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.clickCount == 2) {
            var cell = inventoryGrid.GetCellPosition(Input.mousePosition);
            var slot = inventoryGrid.cells[cell.x, cell.y];
            if (slot != null) {
                slot.Data.Use();
            }
        }
    }
}
