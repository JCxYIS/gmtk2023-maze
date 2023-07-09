using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameUI : MonoBehaviour
{
    [SerializeField] Text _scoreText;
    [SerializeField] Text _timeText;
    [SerializeField] Text _wallsText;

    [SerializeField] Text _notificationText;

    List<(float, string)> _notifications = new List<(float, string)>();

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        _scoreText.text = "Scor: "+ GameController.Instance.Score.ToString("0");
        _timeText.text = GameController.Instance.GameTime.ToString("0.0");
        _wallsText.text = GameController.Instance.Walls.ToString();

        _notificationText.text = "";
        for(int i = 0; i < _notifications.Count; i++)
        {
            if(Time.time - _notifications[i].Item1 >= 2f)
            {
                _notifications.RemoveAt(i);
                i--;
                continue;
            }

            _notificationText.text += _notifications[i].Item2 + "\n";
        }
    }

    public void AddNotification(string notif)
    {
        _notifications.Add((Time.time, notif));
    }
}