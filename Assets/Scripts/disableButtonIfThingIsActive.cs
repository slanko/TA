using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class disableButtonIfThingIsActive : MonoBehaviour
{
    [SerializeField] Button theButton;
    [SerializeField] GameObject theThing;

    // Update is called once per frame
    void Update()
    {
        if(theThing.activeSelf == true)
        {
            theButton.interactable = false;
        }
        else
        {
            theButton.interactable = true;
        }
    }
}
