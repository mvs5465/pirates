using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FishingLootManager : MonoBehaviour
{
    public static Func<Item> GetRandomItem;
    public List<FishingLoot> fishingLoot;

    public void Awake()
    {
        fishingLoot = fishingLoot.OrderBy(item => item.DropChance).ToList();
        GetRandomItem += RandomItem;
    }

    public Item RandomItem()
    {
        foreach (FishingLoot lootItem in fishingLoot)
        {
            if (UnityEngine.Random.Range(0f, 1f) < lootItem.DropChance)
            {
                return lootItem.ToItem();
            }
        }
        return null;
    }
}