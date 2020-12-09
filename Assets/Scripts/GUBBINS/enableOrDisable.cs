using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableOrDisable : MonoBehaviour
{   
    public void enableThis()
    {
        gameObject.SetActive(true);
    }

    public void disableThis()
    {
        gameObject.SetActive(false);
    }
}
