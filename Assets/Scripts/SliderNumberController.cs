using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderNumberController : MonoBehaviour
{
    public Slider slider;
    public TMP_Text number;

    void Start()
    {
        // Subscribe to the slider's value changed event
        slider.onValueChanged.AddListener(UpdateText);
        number.text = slider.value.ToString();
    }

    void UpdateText(float value)
    {
        // Update the text to display the current slider value
        number.text = Mathf.RoundToInt(value).ToString();
    }
}
