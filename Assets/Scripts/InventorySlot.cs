using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour {
    
    public Image icon;
    public Button removeButton;

    public Item item;

    public void AddItem (Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 1f);
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

    public void AddToHotBar()
    {
        if (item != null)
        {
            Debug.Log("Added: " + item.name + " to hotbar.");
            bool added = GameManager.instance.hotbar.AddItem(item);
            if(added)
                item.RemoveFromInventory();
        }
    }

    public void AddToInventory()
    {
        if (item != null)
        {
            Debug.Log("Added: " + item.name + " to Inventory.");
            bool added = Inventory.instance.Add(item);
            if (added)
               GameManager.instance.hotbar.RemoveItem(item);
            
        }
    }

    public void Unquip(int i)
    {
        EquipmentManager.instance.UnEquip(i);
    }

    public void UseItem ()
    {
        // Click an item in the inventory

        if (item != null)
        {
            // Use Item
            item.Use();
        }
    }
}
