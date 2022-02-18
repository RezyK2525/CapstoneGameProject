using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour
{

    /*public Transform itemsParent;*/
    public GameObject hotbar;

    // To cache inventory to run faster
    Inventory inventory;
    InventorySlot[] hotbarSlots;

    public const int HOTBAR_COUNT = 5;
    // Equipment
    //public WeaponSlot weaponSlot;


    void Start()
    {
        DontDestroyOnLoad(gameObject);

        inventory = Inventory.instance;
        // inventory.OnItemChangedCallback += UpdateUI;

        hotbarSlots = GetComponentsInChildren<InventorySlot>();
        Debug.Log("hotbar: " + hotbarSlots[1]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            //Debug.Log("Using slot 1");
            hotbarSlots[0].UseItem();
        }
        else if (Input.GetKeyDown("2"))
        {
            hotbarSlots[1].UseItem();
        }
        else if (Input.GetKeyDown("3"))
        {
            hotbarSlots[2].UseItem();
        }
        else if (Input.GetKeyDown("4"))
        {
            hotbarSlots[3].UseItem();
        }
        else if (Input.GetKeyDown("5"))
        {
            hotbarSlots[4].UseItem();
        }
    }



    /*void UpdateUI()
    {
        Debug.Log("UPDATING HOTBAR");

        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                hotbarSlots[i].AddItem(inventory.items[i]);
            }
            else
            {
                hotbarSlots[i].ClearSlot();
            }
        }
    }*/


    public void AddItem(Item item)
    {
        
        for (int i = 0; i < HOTBAR_COUNT; i++)
        {
            if (hotbarSlots[i].isEmpty())
            {
                hotbarSlots[i].AddItem(item);
                break;
            }
        }
       
    }


}
