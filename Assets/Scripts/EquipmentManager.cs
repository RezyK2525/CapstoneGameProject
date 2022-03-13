using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public Equipment[] currentEquipment;

    public delegate void OnEquipmentChanged (Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    // Canvas
    public GameObject canvas;

    // Update UI Slots for Equipment Tab
    public InventorySlot weaponSlot;
    public InventorySlot spellSlot;
    public InventorySlot AbilitySlot;

    // Weapon Holder
    public Transform weaponHolder;

    void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];


        canvas = GameObject.Find("Canvas");

        weaponSlot = canvas.transform.GetChild(2).GetChild(3).GetChild(0).GetChild(0).GetComponent<InventorySlot>();
        spellSlot = canvas.transform.GetChild(2).GetChild(3).GetChild(0).GetChild(1).GetComponent<InventorySlot>();
        AbilitySlot = canvas.transform.GetChild(2).GetChild(3).GetChild(0).GetChild(2).GetComponent<InventorySlot>();
    }

        // Equip a new Item
        public void Equip (Equipment newItem)
        {
        // Find what slot the item fits
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        // If there's an item already in the slot
        // Swap with this item
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
            if (GameManager.instance.isMultiplayer)
                PhotonNetwork.Destroy(weaponHolder.GetChild(0).gameObject);
            else
                Destroy(weaponHolder.GetChild(0).gameObject);
        }

        // An item has been equipped
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        // Insert item into the slot
        currentEquipment[slotIndex] = newItem;

        // Equip Weapon
        if (slotIndex == 0)
        {
            /// Put the Sword in the player's Hand
            Vector3 pos = weaponHolder.position;

            // Create new Item and place it in the weapon holder
            GameObject Sword = Instantiate(newItem.prefab, pos, Quaternion.identity, weaponHolder);
            Sword.gameObject.GetComponent<BoxCollider>().enabled = false;
            Sword.gameObject.GetComponent<ItemPickup>().enabled = false;
            Sword.gameObject.name = "weapon";

            weaponSlot.AddItem(newItem);
        }
        else if (slotIndex == 1)
        {
            spellSlot.AddItem(newItem);
        }
        else if(slotIndex == 2)
        {
            AbilitySlot.AddItem(newItem);
        }

    }

    public void UnEquip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {

            if(weaponHolder.childCount > 0)
            {
                GameObject oldWeapon = weaponHolder.GetChild(0).gameObject;
                Destroy(oldWeapon);
            }


            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;
            
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

            if (slotIndex == 0)
            {
                Destroy(weaponHolder.GetChild(0).gameObject);
                weaponSlot.ClearSlot();
            }
            else if (slotIndex == 1)
            {
                spellSlot.ClearSlot();
            }
            else if (slotIndex == 2)
            {
                AbilitySlot.ClearSlot();
            }

        }
    }

    public void UnEquipall()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            UnEquip(i);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnEquipall();
        }

       
    }
}
