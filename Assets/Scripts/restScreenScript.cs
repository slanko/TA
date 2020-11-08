using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class restScreenScript : MonoBehaviour
{
    [SerializeField]
    Text titleText1, titleText2;
    [SerializeField]
    Text descriptionText;
    [SerializeField]
    Image sleepySprite;

    float restHealRate;


    //god stuff amen
    GameObject GOD;
    godPointToThing gPTT;
    private void Start()
    {
        GOD = GameObject.Find("GOD");
        gPTT = GOD.GetComponent<godPointToThing>();
    }
    //end of prayers

    public void populateRestScreen(restStopData rSD)
    {
        titleText1.text = rSD.stopName;
        titleText2.text = rSD.restDesc;
        descriptionText.text = rSD.stopDesc;
    }
}
