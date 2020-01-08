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

    IEnumerator GameEnumerator()
    {
        while(_counter < GameDuration)
        {
            yield return new WaitForSeconds(1);
            SpawnManager.Instance.SpawnEnemyFighter();
            _counter++;

            if(Player.Instance == null)
            {
                Time.timeScale = 0;
                GameOverPanel.Instance.gameObject.SetActive(true);
                yield return new WaitUntil(()=>Player.Instance != null);
                Time.timeScale = 1;
            }
        }
    }
}