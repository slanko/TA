using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class skyboxBuddyScript : MonoBehaviour
{
    [SerializeField] Color skyboxColour, fogColour, ambientColour;
    void Update()
    {
        RenderSettings.skybox.SetColor("_Tint", skyboxColour);
        RenderSettings.fogColor = fogColour;
        RenderSettings.ambientLight = ambientColour;
    }
}
