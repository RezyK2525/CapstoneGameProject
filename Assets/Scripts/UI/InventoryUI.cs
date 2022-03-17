using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    public Transform itemsParent;
    public GameObject inventoryUI;
    public GameObject crossHair;
    
    // To cache inventory to run faster
    Inventory inventory;
    InventorySlot[] slots;
    public bool isOpen = false;

    // Equipment
    //public WeaponSlot weaponSlot;


    void Start()
    {
        // DontDestroyOnLoad(gameObject);

        inventory = Inventory.instance;
        inventory.OnItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetButtonDown("Inventory"))
        {
            if (isOpen)
            {
                GameManager.instance.player.allowAttack = true;
                GameManager.instance.player.cursorState();
                isOpen = false;
                
            }
            else
            {
                GameManager.instance.player.allowAttack = false;
                GameManager.instance.player.cursorState();
                isOpen = true;
            }
            UpdateUI();
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            crossHair.SetActive(!crossHair.activeSelf);
        }
    }

    void UpdateUI()
    {
        Debug.Log("UPDATING UI");

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }


   // public void EquipWeapon(Equipment equipment)
   // {
  //      weaponSlot.AddItem(equipment);
   // }

}
