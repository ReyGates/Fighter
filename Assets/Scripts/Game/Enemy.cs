using UnityEngine;
using System.Collections;

public class Enemy : BaseShip<EnemyShipData, Enemy>
{
    protected override void Awake()
    {
        base.Awake();

        Data.Speed = Random.Range(Data.Speed/2, Data.Speed);
    }

    protected override void Update()
    {
        base.Update();
        MoveAI();
        _fire = true;
    }

    protected virtual void MoveAI()
    {
        Vector3 newPos = transform.position;
        newPos.x -= 1;
        Move(newPos);
    }

    public override void OnGetHit(Bullet bullet)
    {
        if(bullet.BulletType == BulletTypeEnum.Player)
        {
            Data.Health -= bullet.Damage;
            Player.Instance.Data.Power += 2;
        }

        base.OnGetHit(bullet);
    }
}
