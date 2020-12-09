using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sublist Data", menuName = "Trucker's Atlas/Sublist Data")]
public class sublistData : MonoBehaviour
{
    public List<eventStruct> sublist;
    [System.NonSerialized] public List<eventStruct> workingList;

    private void Awake()
    {
        foreach(eventStruct ev in sublist)
        {
            workingList.Add(ev);
        }
    }
}
