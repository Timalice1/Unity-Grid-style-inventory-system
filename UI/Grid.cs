using UnityEngine;
using UnityEngine.EventSystems;

public class Grid : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    #region Fields
    //Parameters
    [SerializeField] private Vector2Int cellSize;

    //References
    private RectTransform rectTransform;

    //Properties
    public bool IsActive { get; private set; }
    public Vector2Int gridSize { get; private set; }
    public UI_Slot[,] cells { get; private set; }

    #endregion

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        gridSize = new Vector2Int((int)rectTransform.sizeDelta.x / cellSize.x, (int)rectTransform.sizeDelta.y / cellSize.y);
        cells = new UI_Slot[gridSize.x, gridSize.y];
    }

    #region Public methods
    public Vector2Int GetCellPosition(Vector2 mousePosition) {
        Vector2 cursorPosOnGrid = new Vector2();
        cursorPosOnGrid.x = mousePosition.x - rectTransform.position.x;
        cursorPosOnGrid.y = rectTransform.position.y - mousePosition.y;

        var cellPos = new Vector2Int();
        cellPos.x = (int)cursorPosOnGrid.x / cellSize.x;
        cellPos.y = (int)cursorPosOnGrid.y / cellSize.y;

        return cellPos;
    }

    public void PlaceSlot(UI_Slot slot) {

        Vector2Int pos = GetPosition(slot);

        //Place a slot on the grid
        var slotTransform = slot.GetComponent<RectTransform>();
        slotTransform.parent = rectTransform;
        slotTransform.localPosition = new Vector2(cellSize.x * pos.x, -(cellSize.y * pos.y));

        //Store a slot in cells array
        for (int x = pos.x; x < slot.Data.Size.x + pos.x; x++) {
            for (int y = pos.y; y < slot.Data.Size.y + pos.y; y++) {
                cells[x, y] = slot;
            }
        }

        //Change a size of slot
        slotTransform.sizeDelta = slot.Data.Size * cellSize;
    }

    public void Clear() {
        foreach (Transform t in transform)
            Destroy(t.gameObject);
        cells = new UI_Slot[gridSize.x, gridSize.y];
    }
    #endregion

    #region Private methods
    private Vector2Int GetPosition(UI_Slot slot) {

        int x = 0;
        int y = 0;

        while (cells[x, y] != null || CheckBounds(slot.Data.Size, new Vector2Int(x, y))) {
            x++;
            if (gridSize.x < x + slot.Data.Size.x) {
                x = 0;
                y++;
            }
        }

        return new Vector2Int(x, y);
    }

    private bool CheckBounds(Vector2Int size, Vector2Int pos) {
        UI_Slot slot = null;

        for (int x = 0; x < size.x; x++) {
            for (int y = 0; y < size.y; y++) {
                if(cells[x + pos.x, y + pos.y] != null) {
                    slot = cells[x + pos.x, y + pos.y];
                }
            }
        }

        return slot != null;
    }
    #endregion

    #region Interfaces implementation
    public void OnPointerEnter(PointerEventData eventData) {
        IsActive = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        IsActive = false;
    }
    #endregion
}
