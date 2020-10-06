using System.Collections;
using System.Collections.Generic;
using UnityEngine;





[CreateAssetMenu(fileName = "Shop Data", menuName = "Trucker's Atlas/Shop Data")]
public class shopData : ScriptableObject
{
    public string shopOwnerName;
    [Multiline(4)]
    public string shopDescription, shopFlavourText;

    public Sprite vendorSprite;

    [Multiline(3)]
    public string[] shopTalk;
}
