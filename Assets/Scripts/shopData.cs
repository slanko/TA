using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct stockItem
{
    public globalValuesData.itemType stockType;
    public int stockAmount;
    public float stockValueMult;
}


[CreateAssetMenu(fileName = "Shop Data", menuName = "Trucker's Atlas/Shop Data")]
public class shopData : ScriptableObject
{
    public globalValuesData.factionType faction;
    public string shopOwnerName;
    [Multiline(4)]
    public string shopDescription, shopFlavourText;

    public Sprite vendorSprite;

    [Multiline(3)]
    public string[] shopTalk;

    public List<stockItem> shopStock;
}
