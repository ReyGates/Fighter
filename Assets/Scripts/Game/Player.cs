using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : BaseShip<PlayerShipData, Player>
{
    public MeshRenderer ForceFieldRenderer;

    private Animator _animator;

    protected override void Awake()
    {
        base.Awake();

        _animator = GetComponent<Animator>();

        SwitchShield();
    }

    protected override void Update()
    {
        base.Update();

        PlayerInputUpdate();
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
        if(bullet.BulletType != Data.ShieldType)
        {
            Data.Health -= bullet.Damage;
        }

        Data.Power += 0.5f;

        base.OnGetHit(bullet);
    }
}