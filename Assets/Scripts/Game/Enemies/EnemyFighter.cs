using UnityEngine;
using System.Collections;

public class EnemyFighter : Enemy
{
    public Transform Turret;

    public int TurretAmmo = 5;
    public float Cooldown = 1;
    public float DelayPerFire = 0.25f;

    private float _cooldownCounter;
    private float _delayCounter;
    private int _turretAmmoCounter; 

    protected override void Start()
    {
        base.Start();

        _turretAmmoCounter = TurretAmmo;
    }

    protected override void Update()
    {
        base.Update();

        Turret.LookAt(Player.Instance.transform);

        if(_turretAmmoCounter > 0)
        {
            if(_delayCounter <= 0)
            {
                FireBullet(Turret);
                _delayCounter = DelayPerFire;
            }
            else
            {
                _delayCounter -= Time.deltaTime;
            }
        }
        else
        {
            _cooldownCounter += Time.deltaTime;

            if(_cooldownCounter >= Cooldown)
            {
                _turretAmmoCounter = TurretAmmo;
                _cooldownCounter = 0;
            }
        }
    }
}
