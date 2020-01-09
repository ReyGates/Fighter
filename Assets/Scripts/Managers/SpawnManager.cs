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
        Boss = Instantiate(BossPrefab, new Vector3(15, 0, 0), BossPrefab.transform.rotation, EnemyParent);
        EnemyList.Add(Boss);
    }

    public void SpawnPlayer()
    {
        if (Player.Instance != null)
            Player.Instance.Destroy();

        foreach (var bullet in BulletParent.GetComponentsInChildren<Bullet>())
        {
            Destroy(bullet.gameObject);
        }

        Instantiate(PlayerPrefab, Vector3.zero, PlayerPrefab.transform.rotation);
    }

    public void DestroyAllEnemies()
    {
        for(int i = 0; i < EnemyList.Count; i++)
        {
            EnemyList[i].Data.Health = 0;
        }

        if(Boss != null)
            Boss.Data.Health = 0;

        EnemyList.Clear();
    }
}
