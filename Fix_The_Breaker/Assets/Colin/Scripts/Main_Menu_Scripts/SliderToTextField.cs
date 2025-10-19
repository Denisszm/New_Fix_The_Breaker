using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderToTextField : MonoBehaviour
{
    public TMP_InputField Input;
    public Slider Slider;
    private int value;
    private int sliderValue;
    private float sliderToSring;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LimitValue()
    {
        if (int.TryParse(Input.text, out value))
        {
            if (value >= 100)
            {
                value = 100;
                Input.text = "100";
            }
            else if (value <= 0)
            {
                value = 0;
                Input.text = "0";
            }
        }
    }

    public void SetSliderToInputValue()
    {
        if (int.TryParse(Input.text, out sliderValue))
        {
            Slider.value = sliderValue;
        }
    }

    public void SetInputValueToSlider()
    {
        sliderToSring = Slider.value;
        string result = Convert.ToString(sliderToSring);
        Input.text = result;
    }
}
