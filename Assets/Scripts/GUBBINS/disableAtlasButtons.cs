using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class disableAtlasButtons : MonoBehaviour
{
    [SerializeField] GameObject objectToCheck;
    Button meButton;

    private void Start()
    {
        meButton = GetComponent<Button>();
    }

    private void Update()
    {
        if(objectToCheck.activeSelf == false)
        {
            meButton.interactable = false;
        }
        else
        {
            meButton.interactable = true;
        }
    }
}
