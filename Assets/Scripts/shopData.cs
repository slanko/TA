using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [TextArea]
    public string shopDescription, shopFlavourText;

    public Sprite vendorSprite;

    [TextArea]
    public string[] shopTalk;

    [SerializeField]
    private List<stockItem> baseShopStock;
    [SerializeField]
    private List<stockItem> shopStock;

    public List<stockItem> GetBaseShopStock()
    {
        return baseShopStock.ToList();
    }

    public List<stockItem> GetShopStock()
    {
        return shopStock.ToList();
    }

    public void SetShopStock (List<stockItem> stock)
    {
        shopStock = stock;
    }
}
