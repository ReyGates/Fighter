using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int GameDuration = 30;

    private int _counter = 0;

    private Coroutine _coroutine;

    private void Start()
    {
        StartGame();
        StartCoroutine(CheckPlayerEnumerator());
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        _counter = 0;
        SpawnManager.Instance.SpawnPlayer();
        _coroutine = StartCoroutine(GameEnumerator());
    }

    public void ResumeGame()
    {
        SpawnManager.Instance.SpawnPlayer();
    }

    public void RestartGame()
    {
        StopCoroutine(_coroutine);
        SpawnManager.Instance.DestroyAllEnemies();
        StartGame();
    }

    IEnumerator CheckPlayerEnumerator()
    {
        while (true)
        {
            yield return new WaitUntil(()=>Player.Instance == null);
            Time.timeScale = 0;
            GameOverPanel.Instance.ShowGameOver();
            yield return new WaitUntil(() => Player.Instance != null);
            Time.timeScale = 1;
        }
    }

    IEnumerator GameEnumerator()
    {
        while(_counter < GameDuration)
        {
            yield return new WaitForSeconds(1);
            SpawnManager.Instance.SpawnEnemyFighter();
            _counter++;
        }

        yield return new WaitUntil(()=>SpawnManager.Instance.EnemyList.Count == 0);

        SpawnManager.Instance.SpawnEnemyBoss();
        //yield return new WaitUntil(()=>SpawnManager.Instance.Boss == null);
        int random = 0;
        while(SpawnManager.Instance.Boss != null)
        {
            random = Random.Range(0, 100);
            if(random <= 20)
            {
                if(SpawnManager.Instance.Boss.Data.Health <= (SpawnManager.Instance.Boss.MaxHealth / 2))
                    SpawnManager.Instance.SpawnEnemyFighter();
            }

            yield return new WaitForSeconds(0.5f);
        }

        Time.timeScale = 0;
        GameOverPanel.Instance.ShowVictory();
    }
}