using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
struct repDescription
{
    public int repAmount;
    [Multiline (3)] public string repDesc;
}

public class tabScript : MonoBehaviour
{
    [SerializeField] Slider banditRepSlider, freeTradeRepSlider, corpRepSlider;
    [SerializeField] Text banditRepText, freeTradeRepText, corpRepText;
    [SerializeField] repDescription[] banditRepDescriptions, freeTradeRepDescriptions, corpRepDescriptions;
    [SerializeField] Text banditRepDescText, freeTradeRepDescText, corpRepDescText;
    [SerializeField] GameObject inventoryItemEntry, inventoryZone;
    public List<GameObject> entryList;
    playerResources pR;
    GameObject GOD;

    [SerializeField] Animator healthButtonAnim;

    private void Start()
    {
        GOD = GameObject.Find("GOD");
        pR = GOD.GetComponent<playerResources>();
    }

    private void Update()
    {
        banditRepSlider.value = pR.banditRep;
        banditRepText.text = pR.banditRep.ToString();

        freeTradeRepSlider.value = pR.freeTradeRep;
        freeTradeRepText.text = pR.freeTradeRep.ToString();

        corpRepSlider.value = pR.corporationRep;
        corpRepText.text = pR.corporationRep.ToString();
    }

    public void populateInventoryTab()
    {
        foreach(GameObject objectToKill in entryList)
        {
            Destroy(objectToKill);
        }
        entryList.Clear();
        foreach (playerResources.InventoryEntry entry in pR.playerInventory)
        {
            var iIS = Instantiate(inventoryItemEntry, inventoryZone.transform).GetComponent<inventoryItemScript>();
            iIS.entryName.text = entry.entryType.ToString();
            
            iIS.entryAmount.text = "" + (int)entry.amountHeld;
            entryList.Add(iIS.gameObject);
        }

        //put the health button up or down if the player has or doesn't have med sups
        if(pR.checkForItem(globalValuesData.itemType.MEDSUPS) == true)
        {
            healthButtonAnim.SetBool("state2", true);
        }
        else
        {
            healthButtonAnim.SetBool("state2", false);
        }
    }
}
