using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tradeItemScript : MonoBehaviour
{

    [SerializeField] Slider valueSlider;
    [SerializeField] Text valueText;

    // Update is called once per frame
    void Update()
    {
        valueText.text = valueSlider.value.ToString();
    }
}
