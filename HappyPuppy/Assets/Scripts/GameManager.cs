using System;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public const float WAIT_TIME = 4;
    public const float SLOW_WAIT_TIME = 8;

    [Header("Scoring")]
    public int happyPoints;
    public TMP_Text scoreText;
    public float nextSceneTimer = 3;

    private float _lastScore;
    private int nextSceneVar = 0;
    private bool moveToNextScene = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        scoreText.text = "Score: " + happyPoints;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);

        if(happyPoints >= 10)
        {
            moveToNextScene = true;
        }

        if (moveToNextScene)
        {
            if (nextSceneTimer < 0)
            {
                MoveToNextScene();
            }
            else
            {
                nextSceneTimer -= Time.deltaTime;
            }
        }
    }

    public void IncreaseOfHappiness()
    {
        if(Time.time - _lastScore < WAIT_TIME)
            return;
        _lastScore = Time.time;
        happyPoints += 1;
        scoreText.text = "Score: " + happyPoints;
    }

    public void MoveToNextScene()
    {
        nextSceneVar += 1;
        SceneManager.LoadScene("End");
    }
}
