using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static Action<Item, int> GiveItem;
    public static Action<Item> TakeItem;
    public static Func<Dictionary<Item, int>> GetItemList;

    public Dictionary<Item, int> InventoryItems;

    private void Awake()
    {
        InventoryItems = new();
        GiveItem += AddItem;
        TakeItem += RemoveItem;
        GetItemList += GetItems;
    }

    public Dictionary<Item, int> GetItems()
    {
        return InventoryItems;
    }

    public void AddItem(Item item, int amount)
    {
        if (item == null) return;
        if (InventoryItems.ContainsKey(item))
        {
            InventoryItems[item] += amount;
        }
        else
        {
            InventoryItems.Add(item, 1);
        }
        UIController.NotifyRedraw();
    }
    public void RemoveItem(Item item)
    {
        InventoryItems.Remove(item);
        UIController.NotifyRedraw();
    }
}