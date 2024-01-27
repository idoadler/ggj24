using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private const float WAIT_TIME = 1;
    
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
