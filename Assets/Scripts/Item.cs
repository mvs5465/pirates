using System;
using UnityEngine;

public class Item
{
    public string ItemName { get; private set; }
    public Sprite Sprite { get; private set; }

    public Item(string name, Sprite sprite)
    {
        ItemName = name;
        Sprite = sprite;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ItemName, Sprite);
    }

        public override bool Equals(object other)
    {
        return other is Item otherItem && ItemName == otherItem.ItemName && Sprite == otherItem.Sprite;
    }
}