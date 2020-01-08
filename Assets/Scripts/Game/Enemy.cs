using UnityEngine;
using System.Collections;

public class Enemy : BaseShip<EnemyShipData, Enemy>
{
    protected override void Awake()
    {
        base.Awake();

        Data.Speed = Random.Range(Data.Speed/2, Data.Speed);
    }

    protected virtual void Update()
    {
        _fire = true;

        Vector3 newPos = transform.position;
        newPos.x -= 1;
        Move(newPos);
    }

    public override void OnGetHit(Bullet bullet)
    {
        base.OnGetHit(bullet);

        if(bullet.BulletType == BulletTypeEnum.Player)
        {
            Data.Health -= bullet.Damage;
            Player.Instance.Data.Power += 1;
        }

        Destroy(bullet.gameObject);
    }
}
