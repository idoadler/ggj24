using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public const float WAIT_TIME = 3;
    public const float SLOW_WAIT_TIME = 8;

    [Header("Scoring")]
    public int happyPoints;
    public TMP_Text scoreText;
    
    private float _lastScore;
    private static int nextSceneVar;

    public CharScript charScript;

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
        SceneManager.LoadScene(nextSceneVar);
    }

    public void ActivatePowerUp()
    {
        PowerUp();
    }

    IEnumerator PowerUp()
    {
        Debug.Log("Speed");
        charScript.runSpeed += 15;

        yield return new WaitForSeconds(3);

        charScript.runSpeed -= 15;
    }
}
