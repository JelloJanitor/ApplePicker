using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [Header("Dynamic")]
    public int score = 0;

    //static private TextMeshProUGUI highScoreUIText;
    //static private int highScore = 1000;
    //public TextMeshProUGUI highScoreText;

    public TextMeshProUGUI scoreCounterText;
    public TextMeshProUGUI basketCounterText;

    //void Awake()
    //{
    //    highScoreUIText = GetComponent<TextMeshProUGUI>();
    //}

    //static public int SCORE
    //{
    //    get {return highScore;}
    //    private set
    //    {
    //        highScore = value;
    //        if (highScoreUIText != null)
    //        {
    //            highScoreUIText.text = "High score: " + value.ToString("#,0");
    //        }
    //    }
    //}

    //static public void TRY_SET_HIGH_SCORE(int scoreToTry)
    //{
    //    if (scoreToTry <= highScore) return;
    //    highScore = scoreToTry;
    //}

    void Start()
    {
        //
    }

    void Update()
    {
        //if (highScore < score) highScore = score;
        //highScoreText.text = highScore.ToString("High Score: #,0");
        scoreCounterText.text = score.ToString("Score: #,0");
        basketCounterText.text = "Baskets: 3";
    }
}
