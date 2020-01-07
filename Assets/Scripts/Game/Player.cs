using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public ShipData ShipData;

    public float YPosResistance;

    public Transform WeaponTransform;

    public MeshRenderer ForceFieldRenderer;

    private Camera _cam;

    private Vector2 _firstTouchPos, _currentTouchPos;
    private Vector3 _currentEuler;

    protected override void Awake()
    {
        base.Awake();

        _cam = Camera.main;

        SwitchShield();
    }

    private void Start()
    {
        _currentEuler = transform.rotation.eulerAngles;
    }

    private void Update()
    {
        PlayerInputUpdate();
    }

    public void SwitchShield()
    {
        ShipData.ShieldType = ShipData.ShieldType == BulletTypeEnum.Blue ? BulletTypeEnum.Red : BulletTypeEnum.Blue;

        Color shieldColor = Color.white;

        switch(ShipData.ShieldType)
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
            _currentEuler = transform.rotation.eulerAngles;

            if (_currentTouchPos.y > (_firstTouchPos.y + YPosResistance) || _currentTouchPos.y < (_firstTouchPos.y - YPosResistance))
            {
                Vector3 newPos = transform.position;

                if (_currentTouchPos.y > (_firstTouchPos.y + YPosResistance))
                {
                    newPos.y += 1;
                    _currentEuler.x = -15;
                }
                else if (_currentTouchPos.y < (_firstTouchPos.y - YPosResistance))
                {
                    newPos.y -= 1;
                    _currentEuler.x = 15;
                }

                if(Mathf.Abs(newPos.y) >= 4)
                {
                    newPos = transform.position;
                }

                transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * ShipData.Speed);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _currentTouchPos = Vector3.zero;
            _firstTouchPos = Vector3.zero;
            _currentEuler.x = 0;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_currentEuler), Time.deltaTime * 5);
    }
}
