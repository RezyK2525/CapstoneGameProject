using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour {
    
    public Image icon;
    public Button removeButton;

    Item item;

    public void AddItem (Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot ()
    {

        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }
    public bool isEmpty()
    {
        return item == null;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    public void UseItem ()
    {
        if (item != null)
        {
            item.Use();
        }
    }

    public void moveToInventory()
    {
        if (item != null)
        {
            Debug.Log("moving item to inventory");
            Inventory.instance.Add(item);
            ClearSlot();
        }
    }
}
