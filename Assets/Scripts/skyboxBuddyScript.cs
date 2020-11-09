using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class skyboxBuddyScript : MonoBehaviour
{
    [SerializeField] Color skyboxColour, fogColour, ambientColour;
    [SerializeField] Text dateText;
    [SerializeField] float dayNightAnimSpeed;
    Animator anim;

    //stuff needs to happen on the hour
    GameObject GOD;
    godPointToThing gPTT;
    datesAndTimes dAT;
    private void Start()
    {
        anim = GetComponent<Animator>();
        GOD = GameObject.Find("GOD");
        dAT = GOD.GetComponent<datesAndTimes>();
        gPTT = GOD.GetComponent<godPointToThing>();
        gameObject.GetComponent<Animator>().speed = dayNightAnimSpeed;
        anim.Play("daynight", 0, 0.2515f);
        dateText.text = dAT.getTime() + "  |  " + dAT.getDate();
    }


    void Update()
    {
        RenderSettings.skybox.SetColor("_Tint", skyboxColour);
        RenderSettings.fogColor = fogColour;
        RenderSettings.ambientLight = ambientColour;
    }

    public void timeAddOneHour()
    {
        gPTT.theTime.hour++;
        dateText.text = dAT.getTime() + "  |  " + dAT.getDate();
        doHourlyThings();
    }


    void doHourlyThings()
    {
        if(gPTT.PLAYER.currentState == playerScript.playerState.TRAVELLING)
        {
            gPTT.pR.eatFood();
        }
        gPTT.tS.populateInventoryTab();
    }
}
