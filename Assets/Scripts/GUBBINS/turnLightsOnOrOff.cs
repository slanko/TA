using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnLightsOnOrOff : MonoBehaviour
{
    [SerializeField] Material material1, material2;
    [SerializeField] float lightsOnTime, lightsOffTime;
    godPointToThing gPTT;
    MeshRenderer mR;
    int randomTime;
    [SerializeField] GameObject associatedLight;

    private void Awake()
    {
        mR = GetComponent<MeshRenderer>();
        gPTT = GameObject.Find("GOD").GetComponent<godPointToThing>();
        randomTime = Random.Range(-1, 2);
    }

    private void Update()
    {
        if(gPTT.theTime.hour == lightsOnTime + randomTime)
        {
            mR.material = material2;
            if(associatedLight != null)
            {
                associatedLight.SetActive(true);
            }
            randomTime = Random.Range(-1, 2);
        }
        if(gPTT.theTime.hour == lightsOffTime + randomTime)
        {
            mR.material = material1;
            if (associatedLight != null)
            {
                associatedLight.SetActive(false);
            }
            randomTime = Random.Range(-1, 2);
        }
    }
}
