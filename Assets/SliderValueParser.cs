using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderValueParser : MonoBehaviour
{
    public UnityEvent<string> OnParsedValue;
    Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(Parse);
    }

    private void Parse(float sliderValue)
    {
        string v = sliderValue.ToString();
        OnParsedValue?.Invoke(v);
    }
}
