using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genericKillFunction : MonoBehaviour
{
    public void enableOrDisable(bool enable)
    {
        gameObject.SetActive(enable);
    }

    //this sucks but you can't have extra inputs on animation events >:|
    public void explicitDisableForAnimationEvent()
    {
        gameObject.SetActive(false);
    }

    public void killMe()
    {
        Destroy(gameObject);
    }
}
