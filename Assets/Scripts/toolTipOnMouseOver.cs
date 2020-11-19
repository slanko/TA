using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class toolTipOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    godPointToThing gPTT;
    shopScript sC;
    tradeItemScript tIS;
    globalValuesData gVD;
    Text myDescription;
    // Start is called before the first frame update
    void Start()
    {
        gPTT = GameObject.Find("GOD").GetComponent<godPointToThing>();
        sC = gPTT.shopCanvas.GetComponent<shopScript>();
        tIS = GetComponent<tradeItemScript>();
    }
    //get if mouse is over
    bool mouseOn = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(mouseOn == false)
        {
            gPTT.toolTipText.text = setMyDescription(tIS.myType);
            gPTT.toolTipTransform.gameObject.SetActive(true);
        }
        mouseOn = true;
    }

    void Update()
    {
        if(mouseOn == true)
        {
            gPTT.toolTipTransform.position = Input.mousePosition;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOn = false;
        gPTT.toolTipTransform.gameObject.SetActive(false);
    }


    string setMyDescription(globalValuesData.itemType myType)
    {
        string descToReturn = "";
        foreach (globalValuesData.valueDataEntry entry in sC.globalValueFile.globalValues)
        {
            if(entry.item == myType)
            {
                descToReturn = entry.description;
            }
        }
        return descToReturn;
    }
}
