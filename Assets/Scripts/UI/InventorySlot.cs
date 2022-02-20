using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    item item;
    public Image icon;

    public void AddItem(item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

}
