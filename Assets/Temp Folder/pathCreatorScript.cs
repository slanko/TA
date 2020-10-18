using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathCreatorScript : MonoBehaviour
{
    [HideInInspector]
    public Path path;

    public void createPath()
    {
        path = new Path(transform.position);
    }
}
