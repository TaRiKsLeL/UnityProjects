using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameSession : MonoBehaviour
{

    public int score;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<MiniGameSession>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void resetScore()
    {
        score = 0;
    }

    public void AddToScore(int scoreVal)
    {
        score += scoreVal;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
