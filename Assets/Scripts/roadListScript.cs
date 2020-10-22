using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roadListScript : MonoBehaviour
{
    [System.Serializable]
    public struct roadStruct
    {
        public LineRenderer LR;
        public cityScript Location1, Location2;
    }

    public roadStruct[] allRoads;
}
