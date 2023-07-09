using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BgmPlayer : MonoBehaviour
{
    public static BgmPlayer Instance;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);        
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        GetComponent<AudioSource>().Play();
    }
}