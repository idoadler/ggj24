using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        MenuSettings();
    }

    public void MenuSettings()
    {
        GetComponent<AudioSource>().volume = 0.4f;
        GetComponent<AudioHighPassFilter>().enabled = true;
    }

    public void GameSettings()
    {
        GetComponent<AudioSource>().volume = 0.5f;
        GetComponent<AudioHighPassFilter>().enabled = false;
    }
}
