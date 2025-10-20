using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    //static private TextMeshProUGUI highScoreUIText;
    //static private int highScore = 1000;
    //public TextMeshProUGUI highScoreText;

    public TextMeshProUGUI highScoreText;
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

    private void Start()
    {
        ResetUI();
    }

    void OnEnable()
    {
        GameManager.Instance.OnGameStart += ResetUI;
        GameManager.Instance.OnScore += UpdateScoreText;
        GameManager.Instance.OnAppleMiss += UpdateBasketText;
        Debug.Log("Enabled UI");
    }
    void ResetUI()
    {
        UpdateScoreText();
        UpdateBasketText();
        //Debug.Log("Game starting...");
    }

    void UpdateScoreText()
    {
        //if (highScore < score) highScore = score;
        highScoreText.text = GameManager.Instance.highScore.ToString("High Score: #,0");
        scoreCounterText.text = GameManager.Instance.score.ToString("Score: #,0");
    }

    void UpdateBasketText()
    {
        basketCounterText.text = GameManager.Instance.numBaskets.ToString("Baskets: #,0");
    }
}
