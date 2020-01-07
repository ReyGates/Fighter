﻿using UnityEngine;
using System.Collections;

public class BaseShip<T, U> : Singleton<U> where T : BaseShipData where U : MonoBehaviour
{
    public T Data;

    protected void Move(Vector3 newPos)
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
}
