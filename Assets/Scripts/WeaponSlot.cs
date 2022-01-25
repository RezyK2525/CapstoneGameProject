using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    public Image icon;
    public Button unEquipButton;

    Equipment item;
    
    public void AddItem(Equipment newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        unEquipButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        unEquipButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        EquipmentManager.instance.UnEquip(0);
        ClearSlot();
    }
}
