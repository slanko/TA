using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tradeItemScript : MonoBehaviour
{
    public Slider valueSlider;
    [SerializeField] Text valueText, minAmountText, maxAmountText;
    public Text itemName;
    public globalValuesData.itemType myType;

    // Update is called once per frame
    void Update()
    {
        valueText.text = valueSlider.value.ToString();
        minAmountText.text = valueSlider.minValue.ToString();
        maxAmountText.text = valueSlider.maxValue.ToString();
    }
}
