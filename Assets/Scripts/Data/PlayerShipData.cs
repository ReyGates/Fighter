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

            int weaponActiveCount = 1 + Mathf.FloorToInt(_power / (100 / (WeaponDataList.Count - 1)));
            
            for(int i = 0; i < WeaponDataList.Count; i++)
            {
                WeaponDataList[i].IsActive = i < weaponActiveCount;
            }
        }
    }

    public int PowerAmmo = 30;
    public float PowerDamagePerShot = 5;
    public float PowerDelayPerShot = 0.1f;
}