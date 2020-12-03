using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleThingOnOrOff : MonoBehaviour
{
    [SerializeField] GameObject thing;

    public void toggleThing()
    {
        if(thing.activeSelf == true)
        {
            thing.SetActive(false);
        }
        else
        {
            thing.SetActive(true);
        }
    }
}
