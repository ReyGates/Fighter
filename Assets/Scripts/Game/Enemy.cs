using UnityEngine;
using System.Collections;

public class Enemy : BaseShip<EnemyShipData, Enemy>
{
    protected virtual void Update()
    {
        _fire = true;

        Vector3 newPos = transform.position;
        newPos.x -= 1;
        Move(newPos);
    }
}
