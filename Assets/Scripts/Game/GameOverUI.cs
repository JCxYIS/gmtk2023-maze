using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject _gameOverUI;
    [SerializeField] Text _gameOverUIText;
    [SerializeField] Text _gameOverUISeedText;
    [SerializeField] MMF_Player _gguiFeedbacks;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(GameController.Instance.GameOvered)
        {
            _gameOverUI.SetActive(true);
            _gguiFeedbacks.Initialization();
            _gguiFeedbacks.PlayFeedbacks();

            _gameOverUIText.text = $"timE: {GameController.Instance.GameTime.ToString("0.0")}\n" 
                                 + $"Scor: {GameController.Instance.Score.ToString("0")}\n"
                                 + $"toucc blocc: {(GameController.Instance.TouchedBlocks / 121f * 100f).ToString("0.0")}%";

            _gameOverUISeedText.text = $"seed: {MazeController.Seed}";
            enabled = false;
        }
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }
}