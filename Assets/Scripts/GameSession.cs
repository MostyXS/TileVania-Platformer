using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;
    int currentScore=0;

    private void Awake()
    {
        int GSNum = FindObjectsOfType<GameSession>().Length;
        if (GSNum > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
           
        }
        

    }

    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = currentScore.ToString();
    }
    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
            TakeLife();
        else
            ResetGameProgress();
    }

    private void TakeLife()
    {
        playerLives--;
        FindObjectOfType<SceneLoader>().ResetScene();
        livesText.text = playerLives.ToString();

    }

    private void ResetGameProgress()
    {
        FindObjectOfType<SceneLoader>().LoadStartScene();
        Destroy(gameObject);
    }

    public void AddToScore(int points)
    {
        currentScore += points;
        scoreText.text = currentScore.ToString();
    }

}
