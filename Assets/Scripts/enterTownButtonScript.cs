using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enterTownButtonScript : MonoBehaviour
{

    godPointToThing gPTT;
    [SerializeField] Animator anim;
    [SerializeField] GameObject popup;
    // Start is called before the first frame update
    void Start()
    {
        gPTT = GameObject.Find("GOD").GetComponent<godPointToThing>();
        //amen
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gPTT.PLAYER.currentState == playerScript.playerState.STOPPED && popup.activeSelf == false)
        {
            anim.SetBool("state", true);
        }
        else
        {
            anim.SetBool("state", false);
        }
    }
}
