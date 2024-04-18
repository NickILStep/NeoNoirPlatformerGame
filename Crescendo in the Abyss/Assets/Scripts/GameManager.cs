using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;
    private float highestYPosition = 0f;
    private float initialYPosition = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    private const string HighScoreKey = "HighScore";
    private const string GameVersionKey = "GameVersion";
    private const string CurrentGameVersion = "0.0.234"; // Update this with each new version
    public bool isScoreHigher = false;

    void Start()
    {
        LoadHighScore();
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); Gameobject needs to reload with everything else or it breaks
        }
        else
        {
            Destroy(gameObject);
        }
        initialYPosition = GameObject.FindGameObjectWithTag("Player").transform.position.y;
    }

    void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        LoadHighScore();
    }


    public void UpdateScore(float playerYPosition)
    {
        if (playerYPosition > highestYPosition)
        {
            highestYPosition = playerYPosition;
            score = Mathf.FloorToInt((highestYPosition - initialYPosition) * 5);

            if (score > GetHighScore())
            {
                SaveHighScore(score);
                isScoreHigher = true;
            }
        }
    }

    private void SaveHighScore(int newHighScore)
    {
        PlayerPrefs.SetInt(HighScoreKey, newHighScore);
        PlayerPrefs.Save();
    }

    private int GetHighScore()
    {
        return PlayerPrefs.GetInt(HighScoreKey, 0);
    }

    private void LoadHighScore()
    {
        string savedGameVersion = PlayerPrefs.GetString(GameVersionKey, "");

        if (savedGameVersion != CurrentGameVersion)
        {
            // New game version detected, reset the high score
            PlayerPrefs.DeleteKey(HighScoreKey);
            PlayerPrefs.SetString(GameVersionKey, CurrentGameVersion);
            PlayerPrefs.Save();
            Debug.Log("New game version detected. High score reset.");
        }

        int highScore = GetHighScore();
        if (highScore > 0)
        {
            highScoreText.text = "High Score: " + highScore;
        }
    }

    public bool IsNewHighScore()
    {
        return score > GetHighScore();
    }
}
