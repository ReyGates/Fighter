using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class GameOverPanel : Singleton<GameOverPanel>
{
    public Button ResumeButton;
    public Button RestartButton;

    public TextMeshProUGUI CongratulationText;

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

    public void ShowVictory()
    {
        gameObject.SetActive(true);

        CongratulationText.gameObject.SetActive(true);
        RestartButton.interactable = true;
        ResumeButton.interactable = false;
    }

    public void ShowGameOver()
    {
        gameObject.SetActive(true);

        CongratulationText.gameObject.SetActive(false);
        RestartButton.interactable = true;
        ResumeButton.interactable = true;
    }
}
