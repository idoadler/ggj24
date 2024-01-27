using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int happyPoints;
    public TMP_Text scoreText;

    public void IncreaseOfHappiness()
    {
        happyPoints += 1;
        scoreText.text = "Score: " + happyPoints;
    }
}
