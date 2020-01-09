using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class BaseShip<T, U> : Singleton<U>, IBaseShip where T : BaseShipData where U : MonoBehaviour
{
    public T Data;

    public Bullet BulletPrefab;
    public string BulletLayerMask;

    private float _bulletDelayCounter;

    protected bool _fire = false;

    [SerializeField]
    protected BulletDirectionEnum _bulletDirectionEnum;

    protected BulletTypeEnum _bulletType;

    private HealthBarUI _healthBarUI;

    protected Camera _cam;

    private float _uiAlphaTime = 2;
    private float _uiAlphaCounter = 0;

    protected override void Awake()
    {
        base.Awake();

        _cam = Camera.main;
    }

    protected virtual void Start()
    {
        InitializeUI();
        StartCoroutine(FireEnumerator());
    }

    private void InitializeUI()
    {
        _healthBarUI = GuiManager.CreateHealthBarUI();
        _healthBarUI.HealthBarSlider.maxValue = Data.Health;
        _healthBarUI.HealthBarSlider.value = Data.Health;
        _healthBarUI.CanvasGroup.alpha = 0;
    }

    protected virtual void Update()
    {
        UpdateUI();

        if(Data.Health <= 0)
        {
            Destroy();
        }
    }

    public virtual void Destroy()
    {
        Destroy(_healthBarUI.gameObject);
        SpawnManager.Instance.RemoveFromList(gameObject);
    }

    private void UpdateUI()
    {
        Vector3 newPos = _cam.WorldToScreenPoint(transform.position);
        newPos.y -= 20;

        _healthBarUI.RectTransform.position = newPos;

        if(_uiAlphaCounter > 0)
        {
            _healthBarUI.CanvasGroup.alpha += 1f * Time.deltaTime;

            if(_healthBarUI.CanvasGroup.alpha >= 1)
            {
                _healthBarUI.CanvasGroup.alpha = 1;
                _uiAlphaCounter -= Time.deltaTime;
            }
        }
        else
        {
            _uiAlphaCounter = 0;
            if(_healthBarUI.CanvasGroup.alpha > 0)
            {
                _healthBarUI.CanvasGroup.alpha -= 1f * Time.deltaTime;
            }
        }
    }

    protected void Move(Vector3 newPos)
    {
        if (transform.position.x < 25 && transform.position.x > -10)
        {
            newPos.z = 0;

            Vector3 newRot = transform.rotation.eulerAngles;

            newRot.x = newPos.y > transform.position.y ? -15 : 15;
            newRot.x = Mathf.Abs(newPos.y - transform.position.y) <= 0.5f ? 0 : newRot.x;

            if (Mathf.Abs(newPos.y) > 3)
            {
                newPos.y = newPos.y > 0 ? 3 : -3;
            }

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(newRot), Time.deltaTime * 5);
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * Data.Speed);
        }
        else
        {
            Destroy();
        }
    }

    IEnumerator FireEnumerator()
    {
        if (BulletLayerMask == "Enemy")
        {
            int random = Random.Range(1, 3);
            _bulletType = (BulletTypeEnum)random;
        }
        else
        {
            _bulletType = BulletTypeEnum.Player;
        }

        while (true)
        {
            if (_fire)
            {
                foreach (var weapon in Data.WeaponDataList)
                {
                    if(weapon.IsActive)
                        FireBullet(weapon.WeaponTransform, Data.BulletSpeed, null, false, Data.BulletDamage);
                }

                yield return new WaitForSeconds(Data.BulletDelay);
            }

            yield return new WaitUntil(()=>_fire);
        }
    }

    protected virtual void FireBullet(Transform weapon, float speed, Transform target = null, bool followTarget = false, float damage = -1)
    {
        if (Data.Health <= 0)
            return;

        Bullet newBullet = Instantiate(BulletPrefab, weapon.position, BulletPrefab.transform.rotation, SpawnManager.Instance.BulletParent);
        newBullet.gameObject.layer = LayerMask.NameToLayer(BulletLayerMask);
        newBullet.FollowTarget = followTarget;
        newBullet.Speed = speed;
        newBullet.Target = target;
        newBullet.BulletDirection = _bulletDirectionEnum;

        if (damage != -1)
            newBullet.Damage = damage;

        newBullet.BulletType = _bulletType;

        AudioManager.Instance.PlayLaserSFX();
    }

    public virtual void OnGetHit(Bullet bullet)
    {
        Destroy(bullet.gameObject);

        _healthBarUI.HealthBarSlider.value = Data.Health;
        _uiAlphaCounter = _uiAlphaTime;
    }
}
