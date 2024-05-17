using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static Action<Item> GiveItem;
    public static Action<Item> TakeItem;
    public static Func<ReadOnlyCollection<Item>> GetItemList;

    public List<Item> InventoryItems;

    private void Awake()
    {
        InventoryItems = new();
        GiveItem += AddItem;
        TakeItem += RemoveItem;
        GetItemList += GetItems;
    }

    public ReadOnlyCollection<Item> GetItems()
    {
        return InventoryItems.AsReadOnly();
    }

    public void AddItem(Item item)
    {
        InventoryItems.Add(item);
        UIController.NotifyRedraw();
    }
    public void RemoveItem(Item item)
    {
        InventoryItems.Remove(item);
        UIController.NotifyRedraw();
    }
}