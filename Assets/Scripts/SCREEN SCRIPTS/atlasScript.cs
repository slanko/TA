using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class atlasScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool mouseOverAtlas;
    godPointToThing gPTT;

    private void Start()
    {
        gPTT = GameObject.Find("GOD").GetComponent<godPointToThing>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOverAtlas = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOverAtlas = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && mouseOverAtlas == true)
        {
            gPTT.PLAYER.toggleAtlas();
        }
    }
}
