using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAt : MonoBehaviour
{
    [SerializeField] GameObject GOD;
    godPointToThing gPTT;

    void Start()
    {
        GOD = GameObject.Find("GOD");
        gPTT = GOD.GetComponent<godPointToThing>();
    }

    void Update()
    {
        transform.LookAt(gPTT.gameCamera.transform.position);
    }
}
