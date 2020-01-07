using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public Player PlayerPrefab;

    public void SpawnEnemyFighter()
    {

    }

    public void SpawnEnemyBoss()
    {

    }

    public void SpawnPlayer()
    {
        Instantiate(PlayerPrefab, Vector3.zero, PlayerPrefab.transform.rotation);
    }
}
