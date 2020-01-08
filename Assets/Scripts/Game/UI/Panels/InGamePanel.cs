using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGamePanel : Singleton<InGamePanel>
{
    public Slider PowerIndicatorSlider;

    public Button PowerButton;

    public void ChangeShield()
    {
        if (Player.Instance != null)
            Player.Instance.SwitchShield();
    }

    public void PowerShot()
    {
        if (Player.Instance != null)
            Player.Instance.PowerShot();
    }
}
