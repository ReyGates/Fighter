
using System;
using System.Collections.Generic;

[Serializable]
public class BaseShipData
{
    public float Speed;
    public float BulletSpeed;
    public float Health = 10;

    public float BulletDelay = 1;
    public float BulletDamage = 1;

    public List<WeaponData> WeaponDataList;
}
