using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/FishingLoot")]
public class FishingLoot : ScriptableObject
{
    public string ItemName = "";
    public Sprite Sprite;
    public float DropChance = 0;

    public Item ToItem() {
        return new Item(ItemName, Sprite);
    }
}