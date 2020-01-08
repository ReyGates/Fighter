using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider HealthBarSlider;

    public RectTransform RectTransform
    {
        get
        {
            return GetComponent<RectTransform>();
        }
    }
}
