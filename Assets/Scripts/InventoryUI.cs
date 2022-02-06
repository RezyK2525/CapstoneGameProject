using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public Transform itemsParent;
    public GameObject inventoryUI;
    
    // To cache inventory to run faster
    Inventory inventory;
    InventorySlot[] slots;


    // Equipment
    //public WeaponSlot weaponSlot;


    void Start()
    {
        DontDestroyOnLoad(gameObject);

        inventory = Inventory.instance;
        inventory.OnItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            UpdateUI();
            inventoryUI.SetActive(!inventoryUI.activeSelf);

            
  
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
