using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FishingLootManager : MonoBehaviour
{
    public static Func<Item> GetRandomItem;
    public List<FishingLoot> fishingLoot;

    private struct ItemRollRange
    {
        public float RangeMin;
        public float RangeMax;
        public FishingLoot LootItem;
    }

    public void Awake()
    {
        fishingLoot = fishingLoot.OrderBy(item => item.DropChance).ToList();
        GetRandomItem += RandomTotalItem;
    }

    public Item RandomRareItem()
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

    public Item RandomTotalItem()
    {
        List<ItemRollRange> rollList = new();
        float currentStart = 0;
        foreach (FishingLoot lootItem in fishingLoot)
        {
            rollList.Add(new ItemRollRange
            {
                RangeMin = currentStart,
                RangeMax = currentStart + lootItem.DropChance,
                LootItem = lootItem,
            });
            currentStart += lootItem.DropChance;
        }
        float rollValue = UnityEngine.Random.Range(0f, currentStart);
        foreach (ItemRollRange itemRange in rollList)
        {
            if (itemRange.RangeMin <= rollValue && itemRange.RangeMax > rollValue)
            {
                return itemRange.LootItem.ToItem();
            }
        }
        Debug.Log("Failed to roll any item!");
        Debug.Log(string.Format("currentStart = {0}\ntotalItems = {0}\nRoll = {0}", currentStart, rollList.Count, rollValue));
        return null;
    }
}