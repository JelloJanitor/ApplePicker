using System;
using UnityEngine;

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
    public int startBaskets = 3;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;

    // Variables that will change as the game is played
    [Header("Dynamic")]
    public int highScore = 1000;
    public int score;
    public int numBaskets;

    // Execute when script established
    private void Awake()
    {
        // Retrieve the high score if it exists, otherwise create a field for it
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }

        PlayerPrefs.SetInt("HighScore", highScore);
    }

    // Set up game settings for the start menu
    void Start()
    {
        Time.timeScale = 0f;
        numBaskets = startBaskets;
    }

    // Pause or unpause the game time
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

        // Flop paused state variable and notify subscribers
        isPaused = !isPaused;
        OnGamePause?.Invoke();
    }

    // Called by BasketController if an apple intersects with a basket
    public void ItemCaught(GameObject go)
    {
        UpdateScore(go);
        Destroy(go);
    }
    
    // Update the score based on what object was caught with the basket and destroy said object
    public void UpdateScore(GameObject go)
    {
        // If the game object is an apple (base case) ad 100 points
        if (go.CompareTag("Apple"))
        {
            score += 100;

            // Try to update highScore
            if (score > highScore)
            {
                highScore = score;
            }
        }

        // Notify subscribers of score change
        OnScore?.Invoke();
    }

    // Called by AppleController if an apple falls too low
    public void AppleMissed()
    {
        // Decriment the number of baskets and update subscribers
        numBaskets--;
        OnAppleMiss?.Invoke();

        // Check if the game is over
        if (numBaskets == 0)
        {
            // Try to reset the high score if there is a chance highScore was changed
            if (highScore == score)
            {
                TrySetHighScore();
            }
            
            // Reset the game
            ResetGame();
        }
    }

    // Pauses the game and updates dynamic variables
    private void ResetGame()
    {
        TogglePause();

        highScore = PlayerPrefs.GetInt("HighScore");
        score = 0;
        numBaskets = startBaskets;

        // Update subscribers
        OnGameReset?.Invoke();
    }

    // Unpauses the game and updates subscribers
    public void StartGame()
    {
        TogglePause();
        OnGameStart?.Invoke();
    }

    // Tries to set the high score in PlayerPrefs
    private void TrySetHighScore()
    {
        if (highScore == score)
        {
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    // Reset the high score stored in PlayerPrefs
    [Tooltip("Check this box to reset the HighScore in PlayerPrefs")]
    public bool resetHighScoreNow = false;

    private void OnDrawGizmos()
    {
        if (resetHighScoreNow)
        {
            // Immediately uncheck box
            resetHighScoreNow = false;
            // Base high score is 1000
            PlayerPrefs.SetInt("HighScore", 1000);
            // Warn user of highScore reset.
            Debug.LogWarning("PlayerPrefs HighScore reset to 1,000.");
        }
    }
}
