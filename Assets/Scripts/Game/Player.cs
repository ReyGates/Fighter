using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : BaseShip<PlayerShipData, Player>
{
    public MeshRenderer ForceFieldRenderer;

    private Animator _animator;
    private float _immuneCounter = 3;

    protected override void Awake()
    {
        base.Awake();

        _animator = GetComponent<Animator>();
        Data.Power = 0;
        SwitchShield();
    }

    protected override void Update()
    {
        base.Update();

        PlayerInputUpdate();

        if(_immuneCounter > 0)
            _immuneCounter -= Time.deltaTime;
    }

    public void SwitchShield()
    {
        Data.ShieldType = Data.ShieldType == BulletTypeEnum.Blue ? BulletTypeEnum.Red : BulletTypeEnum.Blue;

        Color shieldColor = Color.white;

        switch (Data.ShieldType)
        {
            case BulletTypeEnum.Blue:
                shieldColor = new Color(0, 0, 1, 0.25f);
                break;

            case BulletTypeEnum.Red:
                shieldColor = new Color(1, 0, 0, 0.25f);
                break;
        }

        ForceFieldRenderer.material.SetColor("_Color", shieldColor);
        _animator.SetTrigger("Change Shield");
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void PlayerInputUpdate()
    {
        Vector3 newPos = transform.position;

        if (Input.GetMouseButton(0))
        {
            newPos = Input.mousePosition;

            if (Input.touchCount > 0)
                newPos = Input.touches[0].position;

            newPos = _cam.ScreenToWorldPoint(newPos);

            newPos.x += 1;

            GameObject go = EventSystem.current.currentSelectedGameObject;
            if (go != null)
            {
                if (Input.touchCount < 2)
                {
                    if (go.GetComponent<Button>() != null)
                    {
                        newPos = transform.position;
                    }
                }
            }

            if (newPos.x < -2)
                newPos.x = -2;

            if (newPos.x > 12)
                newPos.x = 12;

            _fire = true;
        }

        if(Input.GetMouseButtonUp(0))
        {
            _fire = false;
        }

        base.Move(newPos);
    }

    public override void OnGetHit(Bullet bullet)
    {
        if (_immuneCounter > 0)
            return;

        if(bullet.BulletType != Data.ShieldType)
        {
            Data.Health -= bullet.Damage;
        }

        Data.Power += 0.5f;

        base.OnGetHit(bullet);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_immuneCounter > 0)
            return;

        if (collision.transform.GetComponent<Enemy>() != null)
            Data.Health = 0;
    }

    public void PowerShot()
    {
        if(Data.Power >= 100)
            StartCoroutine(PowerShotEnumerator());
    }

    private IEnumerator PowerShotEnumerator()
    {
        for(int i = 0; i < Data.PowerAmmo; i++)
        {
            Enemy target = null;

            if (SpawnManager.Instance.EnemyList.Count > 0)
                target = SpawnManager.Instance.EnemyList[Random.Range(0, SpawnManager.Instance.EnemyList.Count)];

            if (target != null)
            {
                FireBullet(transform, 1, target.transform, true, Data.PowerDamagePerShot);
            }
            else
            {
                i--;
            }

            yield return new WaitForSeconds(Data.PowerDelayPerShot);

            Data.Power = 0;
        }
    }
}