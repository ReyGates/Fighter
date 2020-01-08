using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletTypeEnum BulletType;
    public BulletDirectionEnum BulletDirection;

    public float Speed;

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