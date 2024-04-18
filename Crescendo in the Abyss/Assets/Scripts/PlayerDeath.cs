using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerDeath : MonoBehaviour
{
    public Transform cameraPos;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI quitText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI newHighScoreText;
    

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        // Hide the text elements initially
        gameOverText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
        quitText.gameObject.SetActive(false);
        highScoreText.gameObject.SetActive(false);
        newHighScoreText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            float cameraY = cameraPos.position.y;
            float playerY = transform.position.y;

            if (playerY < cameraY - 9.5f)
            {
                Debug.Log("Player Died");
                isDead = true;
                ShowDeathUI();
            }
        }
    }

    void ShowDeathUI()
    {
        // Show the text elements
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        quitText.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(true);

        if (GameManager.Instance.isScoreHigher == true)
        {
            newHighScoreText.gameObject.SetActive(true);
        }
        else
        {
            newHighScoreText.gameObject.SetActive(false);
        }
    }

}