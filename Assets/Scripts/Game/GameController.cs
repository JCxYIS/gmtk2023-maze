using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    bool _gameOvered = false;

    float _score = 0;
    public float Score => _score;

    float _time = 0;
    public float GameTime => _time;

    float _constructCd = 0f;
    public float ConstructCd => _constructCd;

    int _walls = 0;
    public int Walls => _walls;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(_gameOvered)
        {
            return;
        }

        _time += Time.deltaTime;
        _score += Time.deltaTime * 10f;
        _constructCd -= Time.deltaTime;
    }

    // ---

    public void SetConstructCd(float cd = 3f)
    {
        _constructCd = cd;
    }

    public void AddWalls(int delta = 1)
    {
        _walls += delta;
    }
}