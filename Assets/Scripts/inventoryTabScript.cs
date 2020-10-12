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

public class inventoryTabScript : MonoBehaviour
{
    [SerializeField] Slider banditRepSlider, freeTradeRepSlider, corpRepSlider;
    [SerializeField] Text banditRepText, freeTradeRepText, corpRepText;
    [SerializeField] repDescription[] banditRepDescriptions, freeTradeRepDescriptions, corpRepDescriptions;
    [SerializeField] Text banditRepDescText, freeTradeRepDescText, corpRepDescText;
    playerResources pR;
    GameObject GOD;

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

}
