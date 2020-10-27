using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(roadBuddy))]
public class roadBuddyExtension : Editor
{
    roadBuddy roadB;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("align road!!!!"))
        {
            roadB.alignLinesToTerrain();
        }
        if(GUILayout.Button("align map line!!!!"))
        {
            roadB.lineUpMapBuddy();
        }
        if(GUILayout.Button("do it all!!!!"))
        {
            roadB.alignLinesToTerrain();
            roadB.lineUpMapBuddy();
        }
    }

    void OnEnable()
    {
        roadB = (roadBuddy)target;
    }
}