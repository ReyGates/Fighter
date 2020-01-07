using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : BaseShip<PlayerShipData, Player>
{
    public Transform WeaponTransform;

    public MeshRenderer ForceFieldRenderer;

    private Camera _cam;

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
        if (Input.GetMouseButton(0))
        {
            Vector3 newPos = Input.mousePosition;
            newPos = _cam.ScreenToWorldPoint(newPos);

            GameObject go = EventSystem.current.currentSelectedGameObject;

            if (go != null)
            {
                if (go.GetComponent<Button>() != null)
                    newPos = transform.position;
            }

            base.Move(newPos);
        }
    }
}