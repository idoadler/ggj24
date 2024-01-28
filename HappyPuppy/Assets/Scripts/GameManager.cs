using System;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public const float WAIT_TIME = 4;
    public const float SLOW_WAIT_TIME = 8;
    
    public int happyPoints;
    public TMP_Text scoreText;
    
    private float _lastScore;

    private void Awake()
    {
        Instance = this;
    }

    public void IncreaseOfHappiness()
    {
        if(Time.time - _lastScore < WAIT_TIME)
            return;
        _lastScore = Time.time;
        happyPoints += 1;
        scoreText.text = "Score: " + happyPoints;
    }
}
