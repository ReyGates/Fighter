using UnityEngine;

public class Bullet : MonoBehaviour
{
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
            Color selected = Color.yellow;

            switch(value)
            {
                case BulletTypeEnum.Blue:
                    selected = Color.blue;
                    break;

                case BulletTypeEnum.Red:
                    selected = Color.red;
                    break;
            }

            _trailRenderer.material.color = selected;

            _bulletType = value;
        }
    }

    public BulletDirectionEnum BulletDirection;

    public float Speed;

    private TrailRenderer _trailRenderer;

    private void Update()
    {
        if (transform.position.x < 15 && transform.position.x > -5)
        {
            Vector3 newPos = transform.position;
            newPos.x += (int)BulletDirection;
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * Speed);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}