using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MenuController : MonoBehaviour
{
    [SerializeField] InputField _seedInput;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        // if(MazeController.Seed != 0)
        // {
        //     _seedInput.text = MazeController.Seed.ToString();
        // }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {        
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            MazeController.Seed = int.Parse(string.IsNullOrWhiteSpace(_seedInput.text) ? "0" : _seedInput.text);
            SceneManager.LoadScene("Game");
        }
    }
}