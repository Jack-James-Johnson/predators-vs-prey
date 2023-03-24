using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderDisplay : MonoBehaviour
{
    public Slider slider; 
    public TMP_Text textElement;
    
    void Start()
    {
        changeVals();
    }
    public void changeVals()
    {
        // the text element of the value TMP_Text object is converted to the value of the slider
        // with two placeholder 0s. so if the value was 2, then 02 would be displayed
        textElement.text = slider.value.ToString("00");
    } 
    
}