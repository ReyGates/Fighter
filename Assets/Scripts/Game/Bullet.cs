using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage = 1;

    public Gradient BlueBulletColor;
    public Gradient RedBulletColor;
    public Gradient PlayerBulletColor;

    [SerializeField]
    private BulletTypeEnum _bulletType;
    public BulletTypeEnum BulletType
    {
        get
        {
            return _bulletType;
        }

        set
        {
            switch(value)
            {
                case BulletTypeEnum.Blue:
                    TrailRenderer.colorGradient = BlueBulletColor;
                    break;

                case BulletTypeEnum.Red:
                    TrailRenderer.colorGradient = RedBulletColor;
                    break;

                case BulletTypeEnum.Player:
                    TrailRenderer.colorGradient = PlayerBulletColor;
                    break;
            }

            _bulletType = value;
        }
    }

    public BulletDirectionEnum BulletDirection;

    public float Speed;

    public bool FollowTarget = false;

    private Transform _target;
    public Transform Target
    {
        get
        {
            return _target;
        }

        set
        {
            _target = value;

            if (_target != null)
                _direction = _target.position - transform.position;
        }
    }

    public TrailRenderer TrailRenderer;

    private Vector3 _direction;

    private void Update()
    {
        if (transform.position.x < 15 && transform.position.x > -5)
        {
            Vector3 newPos = transform.localPosition;

            if(FollowTarget)
            {
                if(_target == null)
                {
                    if(_bulletType == BulletTypeEnum.Player)
                    {
                        float mostNearest = 0;
                        foreach(var target in SpawnManager.Instance.EnemyList)
                        {
                            float distance = Vector3.Distance(transform.position, target.transform.position);
                            if (mostNearest == 0)
                                mostNearest = distance;
                            else
                            {
                                if(mostNearest > distance)
                                {
                                    mostNearest = distance;
                                    _target = target.transform;
                                }
                            }
                        }
                    }
                }

                if (_target != null)
                    _direction = _target.position - transform.position;

                Speed = Mathf.Lerp(Speed, Speed + 3, Time.deltaTime * 10);
            }

            if (_target != null)
            {
                newPos += _direction.normalized;
            }
            else
            {
                newPos.x += (int)BulletDirection;
            }

            transform.localPosition = Vector3.Lerp(transform.localPosition, newPos, Time.deltaTime * Speed);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        IBaseShip iBaseShip = collision.gameObject.GetComponent<IBaseShip>();

        if(iBaseShip != null)
        {
            iBaseShip.OnGetHit(this);
        }
    }
}