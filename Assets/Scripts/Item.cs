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
}