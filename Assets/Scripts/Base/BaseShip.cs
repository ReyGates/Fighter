using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;

public class BaseShip<T, U> : Singleton<U>, IBaseShip where T : BaseShipData where U : MonoBehaviour
{
    public T Data;

    public Transform WeaponTransform;
    public Bullet BulletPrefab;
    public string BulletLayerMask;

    private float _bulletDelayCounter;

    protected bool _fire = false;

    [SerializeField]
    protected BulletDirectionEnum _bulletDirectionEnum;

    private BulletTypeEnum _bulletType;

    protected virtual void Start()
    {
        StartCoroutine(FireEnumerator());
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
            SpawnManager.Instance.RemoveFromList(gameObject);
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
                FireBullet(WeaponTransform, Data.BulletSpeed);

                yield return new WaitForSeconds(Data.BulletDelay);
            }

            yield return new WaitUntil(()=>_fire);
        }
    }

    protected void FireBullet(Transform weapon, float speed, Transform target = null, bool followTarget = false)
    {
        Bullet newBullet = Instantiate(BulletPrefab, weapon.position, BulletPrefab.transform.rotation, SpawnManager.Instance.BulletParent);
        newBullet.gameObject.layer = LayerMask.NameToLayer(BulletLayerMask);
        newBullet.Speed = speed;
        newBullet.Target = target;
        newBullet.BulletDirection = _bulletDirectionEnum;

        newBullet.BulletType = _bulletType;
    }

    public virtual void OnGetHit(Bullet bullet)
    {
    }
}
