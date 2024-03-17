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

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
    }

    public void UpdateScore(float playerYPosition)
    {
        if (playerYPosition > highestYPosition)
        {
            highestYPosition = playerYPosition;
            score = Mathf.FloorToInt((highestYPosition - initialYPosition) * 5);
        }
    }
}
