using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public Player PlayerPrefab;
    public Enemy BossPrefab;
    public List<Enemy> EnemyPrefabList;

    public Enemy Boss;
    public List<Enemy> EnemyList;

    public Transform EnemyParent;
    public Transform BulletParent;

    public void SpawnEnemyFighter()
    {
        Enemy enemy = EnemyPrefabList[Random.Range(0, EnemyPrefabList.Count)];
        EnemyList.Add(Instantiate(enemy, new Vector3(15, Random.Range(-4f, 4f), 0), enemy.transform.rotation, EnemyParent));
    }

    public void RemoveFromList(GameObject go)
    {
        if(go.GetComponent<Enemy>() != null)
        {
            EnemyList.Remove(go.GetComponent<Enemy>());
        }

        Destroy(go);
    }

    public void SpawnEnemyBoss()
    {

    }

    public void SpawnPlayer()
    {
        Instantiate(PlayerPrefab, Vector3.zero, PlayerPrefab.transform.rotation);
    }
}
