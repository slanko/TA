using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleMaterial : MonoBehaviour
{
    [SerializeField] Material material1, material2;
    MeshRenderer mR;
    bool onOrOff;

    private void Awake()
    {
        mR = GetComponent<MeshRenderer>();
    }
    public void toggleTheMaterial()
    {
        if(onOrOff == false)
        {
            mR.material = material2;
            onOrOff = true;
        }
        else
        {
            mR.material = material1;
            onOrOff = false;
        }
    }
}
