using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletTypeEnum BulletType;

    public float Speed;

    private void Update()
    {
        Vector3 newPos = transform.position;
        newPos.x += 1;
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * Speed);
    }
}