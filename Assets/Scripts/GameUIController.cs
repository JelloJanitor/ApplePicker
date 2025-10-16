using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [Header("Dynamic")]
    public int highScore = 0;
    public int score = 0;

    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI scoreCounterText;
    public TextMeshProUGUI basketCounterText;

    void Start()
    {
        //
    }

    void Update()
    {
        if (highScore < score) highScore = score;
        highScoreText.text = highScore.ToString("High Score: #,0");
        scoreCounterText.text = score.ToString("Score: #,0");
        basketCounterText.text = "Baskets: 3";
    }
}
