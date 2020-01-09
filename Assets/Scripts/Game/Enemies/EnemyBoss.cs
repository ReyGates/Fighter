using UnityEngine;
using System.Collections;

public class EnemyBoss : EnemyFighter
{
    public bool Ready = false;

    public BulletTypeEnum ShieldType = BulletTypeEnum.Player;

    public Transform Shield;

    public Vector3 BossPosition = new Vector3(11, 0, 0);

    public float ShieldActiveTime = 3;
    public float ShieldActiveDelay = 10;

    private float _shieldActiveTimeCounter;
    private float _shieldActiveDelayCounter;

    protected override void Start()
    {
        base.Start();

        _shieldActiveDelayCounter = ShieldActiveDelay;
    }

    protected override void Update()
    {
        if (!Ready)
        {
            Move(BossPosition);

            if (Vector3.Distance(transform.position, BossPosition) < 1)
                Ready = true;

            return;
        }

        ShieldUpdate();

        base.Update();
    }

    protected override void FireBullet(Transform weapon, float speed, Transform target = null, bool followTarget = false, float damage = -1)
    {
        base.FireBullet(weapon, speed, target, followTarget, damage);

        int random = Random.Range(1, 3);
        _bulletType = (BulletTypeEnum)random;
    }

    protected override void MoveAI()
    {
        if (Vector3.Distance(transform.position, BossPosition) < 1)
            BossPosition.y = Random.Range(-4f, 4f);

        Move(BossPosition);
    }

    private void ShieldUpdate()
    {
        if(_shieldActiveTimeCounter > 0)
        {
            _shieldActiveTimeCounter -= Time.deltaTime;
            _shieldActiveDelayCounter = ShieldActiveDelay;
        }
        else
        {
            if(_shieldActiveDelayCounter > 0)
            {
                _shieldActiveDelayCounter -= Time.deltaTime;
            }
            else
            {
                _shieldActiveTimeCounter = ShieldActiveTime;
            }
        }

        Shield.gameObject.SetActive(_shieldActiveTimeCounter > 0);
    }

    public override void OnGetHit(Bullet bullet)
    {
        if(_shieldActiveTimeCounter <= 0 && Ready)
        {
            TurretAmmo += 1;
            TurretBulletSpeed += 0.02f;
            DelayPerFire -= 0.0001f;

            Data.BulletDelay -= 0.0001f;
            Data.BulletSpeed += 0.01f;

            base.OnGetHit(bullet);
        }
        else
        {
            Destroy(bullet.gameObject);
        }
    }

    public override void Destroy()
    {
        base.Destroy();

        GameManager.Instance.RestartGame();
    }
}
