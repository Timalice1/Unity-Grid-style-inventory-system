using UnityEngine;
using UnityEngine.UI;

public class UI_Slot : MonoBehaviour
{
    //References
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _stackLabel;
    
    public ItemData Data { get; private set; }

    public void Set(InventoryItem item) {
        Data = item.data;
        _icon.sprite = item.data.Icon;
        if(item.stackSize <= 1) {
            _stackLabel.SetActive(false);
            return;
        }
        _stackLabel.transform.GetChild(0).GetComponent<Text>().text = item.stackSize.ToString();
    }
}
