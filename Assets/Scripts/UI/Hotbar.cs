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
    private int space = 5;
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

    public bool AddItem(Item item)
    {
        if (space == 0)
            return false;

        for (int i = 0; i < HOTBAR_COUNT; i++)
        {
            if (hotbarSlots[i].isEmpty())
            {
                hotbarSlots[i].AddItem(item);
                space--;
                return true;
            }
        }

        return false;
    }

    public bool RemoveItem(Item item)
    {
        for (int i = 0; i < HOTBAR_COUNT; i++)
        {
            if (hotbarSlots[i].item == item)
            {
                hotbarSlots[i].ClearSlot();
                space++;
                return true;
            }
        }

        return false;
    }
}
