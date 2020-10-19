using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathCreatorScript : MonoBehaviour
{
    [HideInInspector]
    public Path path;

    public Color anchorColor = Color.red;
    public Color controlColor = Color.gray;
    public Color segmentColor = Color.blue;
    public Color selectedSegColor = Color.green;
    public float anchorDiameter = .1f;
    public float controlDiameter = .075f;
    public bool displayControlPoints = true;

    public void createPath()
    {
        path = new Path(transform.position);
    }
}
