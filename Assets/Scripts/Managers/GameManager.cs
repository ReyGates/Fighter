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
        SpawnManager.Instance.SpawnPlayer();
        StartGame();
    }

    public void StartGame()
    {
        _coroutine = StartCoroutine(GameEnumerator());
    }

    IEnumerator GameEnumerator()
    {
        while(_counter < GameDuration)
        {
            yield return new WaitForSeconds(1);
            _counter++;
        }
    }
}