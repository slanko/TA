﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sublist Data", menuName = "Trucker's Atlas/Sublist Data")]
public class sublistData : ScriptableObject
{
    public List<eventStruct> sublist;
}
