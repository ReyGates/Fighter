using UnityEngine;

public class Bullet : MonoBehaviour
{
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

    public Transform Target;

    public TrailRenderer TrailRenderer;

    private Vector3 _direction;

    private void Update()
    {
        if (Target != null)
            _direction = Target.position - transform.position;

        if (transform.position.x < 15 && transform.position.x > -5)
        {
            Vector3 newPos = transform.localPosition;
            newPos.x += (int)BulletDirection;
            transform.localPosition = Vector3.Lerp(transform.localPosition, newPos, Time.deltaTime * Speed);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}