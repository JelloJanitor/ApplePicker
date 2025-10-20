using TMPro;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    // Text fields
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI scoreCounterText;
    public TextMeshProUGUI basketCounterText;
    // Game menu with start game button
    public GameObject gameMenu;

    // Set the UI as soon the game is started
    private void Start()
    {
        UpdateUI();
    }

    // Subscribe functions
    void OnEnable()
    {
        GameManager.Instance.OnGameStart += UpdateUI;
        GameManager.Instance.OnScore += UpdateScoreText;
        GameManager.Instance.OnAppleMiss += UpdateBasketText;
        GameManager.Instance.OnGameReset += ActivateMenu;
    } 

    // Reset the UI with updated information
    void UpdateUI()
    {
        UpdateScoreText();
        UpdateBasketText();
    }

    // Bring back the start game button
    void ActivateMenu()
    {
        gameMenu.SetActive(true);
    }

    // Update the highScore and score text fields
    void UpdateScoreText()
    {
        highScoreText.text = GameManager.Instance.highScore.ToString("High Score: #,0");
        scoreCounterText.text = GameManager.Instance.score.ToString("Score: #,0");
    }

    // Update basket text
    void UpdateBasketText()
    {
        basketCounterText.text = GameManager.Instance.numBaskets.ToString("Baskets: #,0");
    }

    // Start the game and remove the start button from sight
    public void OnStartGameButtonPressed()
    {
        GameManager.Instance.StartGame();
        gameMenu.SetActive(false);
    }
}
