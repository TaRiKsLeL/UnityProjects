using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShowTimer : MonoBehaviour
{
    GameSession gameSession;
    Text text;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = Convert.ToInt32(gameSession.GetTimer()).ToString();
    }
}
