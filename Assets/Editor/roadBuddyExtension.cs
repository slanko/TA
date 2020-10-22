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
    }

    void OnEnable()
    {
        roadB = (roadBuddy)target;
    }
}