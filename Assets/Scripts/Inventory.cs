using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    #endregion

    // delegate trigger to update UI when making changes
    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    // Inventory variables
    public int space = 12;
    public List<Item> items = new List<Item>();

    public bool Add (Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enought room.");
                return false;
            }
            items.Add(item);

            // Update UI
            if (OnItemChangedCallback != null)
                OnItemChangedCallback.Invoke();
        }
        return true;
    }

    public void Remove (Item item)
    {
        items.Remove(item);

        // Update UI
        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
    }
}
