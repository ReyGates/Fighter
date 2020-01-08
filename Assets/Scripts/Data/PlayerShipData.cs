using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerShipData : BaseShipData
{
    public BulletTypeEnum ShieldType;

    private float _power;
    public float Power
    {
        get
        {
            return _power;
        }

        set
        {
            _power = value < 100 ? value : 100;

            InGamePanel.Instance.PowerIndicatorSlider.value = _power;
            InGamePanel.Instance.PowerButton.interactable = _power >= 100;
        }
    }
}