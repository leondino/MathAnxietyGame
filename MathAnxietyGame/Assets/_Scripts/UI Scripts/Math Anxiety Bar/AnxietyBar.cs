using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnxietyBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxMathAnxiety(int mathAnxietyLevel)
    {
        slider.maxValue = mathAnxietyLevel;
        slider.value = mathAnxietyLevel;
    }

    public void SetMathAnxiety(int mathAnxietyLevel)
    {
        slider.value = mathAnxietyLevel;
    }
}
