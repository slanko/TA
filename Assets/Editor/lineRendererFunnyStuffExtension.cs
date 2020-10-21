using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(lineRendererFunnyStuff))]
public class lineRendererFunnyStuffExtension : Editor
{
    lineRendererFunnyStuff LRFS;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("do it!!!!"))
        {
            LRFS.alignLinesToTerrain();
        }
    }

    void OnEnable()
    {
        LRFS = (lineRendererFunnyStuff)target;
    }
}