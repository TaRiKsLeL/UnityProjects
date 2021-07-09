using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingUIManager : MonoBehaviour
{

    public Text rightAttemptsText;
    public Text bufferText;
    public Text timerText;

    private TypingGameManager gameManager;

    public GameObject mainCanvas;
    public GameObject gameOverCanvas;

    void Start()
    {
        gameManager = FindObjectOfType<TypingGameManager>();
    }

    void Update()
    {
        rightAttemptsText.text = "Вірно: " + gameManager.rightAttempts.ToString();
        bufferText.text = gameManager.buffer;
        timerText.text = "Час: " + ((int)gameManager.timerVal).ToString();

    }

    public void GameOver()
    {
        mainCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
    }
}
