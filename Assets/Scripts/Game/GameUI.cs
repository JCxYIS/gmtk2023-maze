using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameUI : MonoBehaviour
{
    [SerializeField] Text _scoreText;
    [SerializeField] Text _timeText;
    [SerializeField] Text _wallsText;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        _scoreText.text = "Scor: "+ GameController.Instance.Score.ToString("0");
        _timeText.text = GameController.Instance.GameTime.ToString("0.0");
        _wallsText.text = GameController.Instance.Walls.ToString();
    }
}