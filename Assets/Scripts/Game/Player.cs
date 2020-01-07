using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseShip<PlayerShipData, Player>
{
    public float YPosResistance, XPosResistance;

    public Transform WeaponTransform;

    public MeshRenderer ForceFieldRenderer;

    private Camera _cam;

    private Vector2 _firstTouchPos, _currentTouchPos;

    protected override void Awake()
    {
        base.Awake();

        _cam = Camera.main;

        SwitchShield();
    }

    private void Update()
    {
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
    }

    private void PlayerInputUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _firstTouchPos = _cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            _currentTouchPos = _cam.ScreenToWorldPoint(Input.mousePosition);

            bool yFlag = Utility.CheckInputTouchPosition(_currentTouchPos.y, _firstTouchPos.y, YPosResistance);
            bool xFlag = Utility.CheckInputTouchPosition(_currentTouchPos.x, _firstTouchPos.x, XPosResistance);

            if (yFlag || xFlag)
            {
                Vector3 newPos = transform.position;

                if (_currentTouchPos.y > (_firstTouchPos.y + YPosResistance))
                {
                    newPos.y += 1;
                }
                else if (_currentTouchPos.y < (_firstTouchPos.y - YPosResistance))
                {
                    newPos.y -= 1;
                }

                if (_currentTouchPos.x > (_firstTouchPos.x + XPosResistance))
                {
                    newPos.x += 1;
                }
                else if (_currentTouchPos.x < (_firstTouchPos.x - XPosResistance))
                {
                    newPos.x -= 1;
                }

                if (Mathf.Abs(newPos.y) >= 4)
                {
                    newPos = transform.position;
                }

                base.Move(newPos);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _currentTouchPos = Vector3.zero;
            _firstTouchPos = Vector3.zero;
        }
    }
}