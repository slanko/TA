using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class genericToolTipScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    godPointToThing gPTT;
    [SerializeField, TextArea] string myDescription;
    // Start is called before the first frame update
    void Start()
    {
        gPTT = GameObject.Find("GOD").GetComponent<godPointToThing>();
    }
    //get if mouse is over
    bool mouseOn = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (mouseOn == false)
        {
            gPTT.toolTipAnim.Play("toolTipFadeIn");
            gPTT.toolTipText.text = myDescription;
            gPTT.toolTipTransform.gameObject.SetActive(true);
        }
        mouseOn = true;
    }

    void Update()
    {
        if (mouseOn == true)
        {
            gPTT.toolTipTransform.position = Input.mousePosition;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOn = false;
        gPTT.toolTipAnim.Play("toolTipFadeOut");
    }
}
