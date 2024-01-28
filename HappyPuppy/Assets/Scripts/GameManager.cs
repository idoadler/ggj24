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
    
    private float _lastScore;
    private static int nextSceneVar = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(happyPoints >= 10)
        {
            MoveToNextScene();
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
        SceneManager.LoadScene(nextSceneVar);
    }

    public void ActivatePowerUp()
    {
        PowerUp();
    }

    IEnumerator PowerUp()
    {
        Debug.Log("Speed");
        CharScript.Instance.runSpeed += 15;

        yield return new WaitForSeconds(3);

        CharScript.Instance.runSpeed -= 15;
    }
}
