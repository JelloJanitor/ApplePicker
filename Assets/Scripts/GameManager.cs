using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    // Establish singleton GameManager
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("GameManager");
                _instance = obj.AddComponent<GameManager>();
                DontDestroyOnLoad(obj);
            }

            return _instance;
        }
    }

    // Establish actions
    public Action OnGameStart;
    public Action OnGamePause;
    public Action OnScore;
    public Action OnAppleMiss;
    public Action OnGameReset;

    // Establish private variables
    private bool isPaused = true; // SET BACK TO FALSE

    // Establish public variables
    [Header("Inscribed")]
    //public GameObject basketPrefab;
    public int startBaskets = 3;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;

    [Header("Dynamic")]
    public int highScore = 1000;
    public int score;

    public int numBaskets;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }

        PlayerPrefs.SetInt("HighScore", highScore);
    }

    void Start()
    {
        Time.timeScale = 0f;
        numBaskets = startBaskets;
        //TogglePause();
        //ResetGame();
        //StartGame(); // REPLACE WHEN MENU INSTALLED
    }

//void CreateBaskets()
//{
//    numBaskets = 0;

//    //basketList = new List<GameObject>();
//    for (int i = 0; i < totalBaskets; i++)
//    {
//        Debug.Log("Incramenting numBaskets");

//        GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
//        //GameObject tBasketGO = Instantiate<GameObject>();
//        Vector3 pos = Vector3.zero;
//        pos.y = basketBottomY + (basketSpacingY * i);
//        tBasketGO.transform.position = pos;
//        //OnBasketAdd?.Invoke();
//        //basketList.Add(tBasketGO);
//    }
//}

// Pause or unpause the time
public void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }

        isPaused = !isPaused;
        OnGamePause?.Invoke();
    }

    public void ItemCaught(GameObject go)
    {
        UpdateScore(go);
        Destroy(go);
    }
    
    // Update the score based on what object was caught with the basket and destroy said object
    public void UpdateScore(GameObject go)
    {
        if (go.CompareTag("Apple"))
        {
            score += 100;

            if (score > highScore)
            {
                highScore = score;
            }
            //HighScoreController.TRY_SET_HIGH_SCORE(gameUI.score);
        }

        OnScore?.Invoke();
    }

    public void AppleMissed()
    {
        numBaskets--;
        OnAppleMiss?.Invoke();

        //Debug.Log("GameManager numBaskets: " + numBaskets);

        if (numBaskets == 0)
        {
            if (highScore == score)
            {
                TrySetHighScore();
            }
            
            ResetGame();
        }
    }

    //public int GetNumBaskets()
    //{
    //    return numBaskets;
    //}

    //public void SetNumBaskets(int numBaskets)
    //{
    //    this.numBaskets = numBaskets;
    //}

    private void ResetGame()
    {
        TogglePause();

        highScore = PlayerPrefs.GetInt("HighScore");
        score = 0;
        numBaskets = startBaskets;

        OnGameReset?.Invoke();
    }

    public void StartGame()
    {
        TogglePause();
        OnGameStart?.Invoke();
    }

    private void TrySetHighScore()
    {
        if (highScore == score)
        {
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    [Tooltip("Check this box to reset the HighScore in PlayerPrefs")]
    public bool resetHighScoreNow = false;

    private void OnDrawGizmos()
    {
        if (resetHighScoreNow)
        {
            resetHighScoreNow = false;
            PlayerPrefs.SetInt("HighScore", 1000);
            Debug.LogWarning("PlayerPrefs HighScoe reset to 1,000.");
        }
    }
}
