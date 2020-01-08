using UnityEngine;
using System.Collections;

public class GameOverPanel : Singleton<GameOverPanel>
{
    protected override void Awake()
    {
        base.Awake();

        gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }

    public void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
    }
}
