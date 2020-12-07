using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventSublistScript : MonoBehaviour
{
    List<eventStruct> eventSublist;

    //explicitly referencing randomeventscript instead of gptt because i don't need to reference a reference in this case i think
    randomEventScript eventMaster;
    private void Awake()
    {
        eventMaster = GameObject.Find("GOD").GetComponent<randomEventScript>();
    }
}
