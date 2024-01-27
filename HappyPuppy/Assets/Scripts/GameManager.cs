using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public const float WAIT_TIME = 3;
    public const float SLOW_WAIT_TIME = 8;
    
    public int happyPoints;
    public TMP_Text scoreText;
    
    private float _lastScore;

    public void IncreaseOfHappiness()
    {
        if(Time.time - _lastScore < WAIT_TIME)
            return;
        _lastScore = Time.time;
        happyPoints += 1;
        scoreText.text = "Score: " + happyPoints;
    }
}
