using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public const float WAIT_TIME = 3;
    public const float SLOW_WAIT_TIME = 8;
    
    public int happyPoints;
    public TMP_Text scoreText;
    
    private float _lastScore;
    private static int nextSceneVar;

    private void Update()
    {
        if(happyPoints >= 10)
        {
            MoveToNextScene();
        }
    }

    public void IncreaseOfHappiness()
    {
        //Compares the last time the player scored with
        if(Time.time - _lastScore < WAIT_TIME)
            return;

        _lastScore = Time.time;

        happyPoints += 1;
        scoreText.text = "Score: " + happyPoints;
    }

    public void MoveToNextScene()
    {
        nextSceneVar += 1;
        SceneManager.LoadScene(0 + nextSceneVar);
    }
}
