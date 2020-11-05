using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class skyboxBuddyScript : MonoBehaviour
{
    [SerializeField] Color skyboxColour, fogColour, ambientColour;
    [SerializeField] Text dateText;
    [SerializeField] int dayHour;
    [SerializeField] float dayNightAnimSpeed;
    Animator anim;

    //stuff needs to happen on the hour
    GameObject GOD;
    godPointToThing gPTT;
    private void Start()
    {
        anim = GetComponent<Animator>();
        GOD = GameObject.Find("GOD");
        gPTT = GOD.GetComponent<godPointToThing>();
        gameObject.GetComponent<Animator>().speed = dayNightAnimSpeed;
        anim.Play("daynight", 0, 0.2515f);
        dateText.text = "TIME: 0" + dayHour.ToString() + ":00";
    }


    void Update()
    {
        RenderSettings.skybox.SetColor("_Tint", skyboxColour);
        RenderSettings.fogColor = fogColour;
        RenderSettings.ambientLight = ambientColour;
    }

    public void timeAddOneHour()
    {
        dayHour++;
        if (dayHour > 24)
        {
            dayHour = 0;
        }
        if (dayHour < 10)
        {
            dateText.text = "TIME: 0" + dayHour.ToString() + ":00";
        }
        else
        {
            dateText.text = "TIME: " + dayHour.ToString() + ":00";
        }
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
