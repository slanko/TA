using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cityScript : MonoBehaviour
{
    public locationData cityLD;
    [SerializeField] Text myText;
    public GameObject cityMarker;
    public MeshRenderer markerRenderer;
    public restStopData rSD;

    //prayers
    GameObject GOD;
    godPointToThing gPTT;
    //amen pt.1
    private void Start()
    {
        GOD = GameObject.Find("GOD");
        gPTT = GOD.GetComponent<godPointToThing>();
        gPTT.cityNameTextList.Add(myText);
        //amen pt. 2
        myText.text = cityLD.cityName;
        markerRenderer = cityMarker.GetComponent<MeshRenderer>();   
        if(cityLD.tradeArea != null)
        {
            cityLD.tradeArea.SetShopStock(cityLD.tradeArea.GetBaseShopStock());
        }
    }
}
