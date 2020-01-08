using UnityEngine;
using System.Collections;

public class Enemy : BaseShip<EnemyShipData, Enemy>
{
    private void Update()
    {
        _fire = true;

        Vector3 newPos = transform.position;
        newPos.x -= 1;
        Move(newPos);
    }
}
