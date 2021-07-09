using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultUIManager : MonoBehaviour
{
    public Text rightAttemptsText;
    public Text falseAttemptsText;
    public Text timerText;

    public Text firstNumText;
    public Text secondNumText;


    public GameObject mainCanvas;
    public GameObject gameOverCanvas;

    private MultGameManager gameManager;

    public void Start()
    {
        gameManager = FindObjectOfType<MultGameManager>();
    }

    public void Update()
    {
        rightAttemptsText.text = "Вірно: " + gameManager.rightAttempts.ToString();
        falseAttemptsText.text = "Невірно: " + gameManager.falseAttempts.ToString();
        timerText.text = "Час: " + ((int)gameManager.timerVal).ToString();

        firstNumText.text = gameManager.firstNum.ToString();
        secondNumText.text = gameManager.secondNum.ToString();
    }

    public void GameOver()
    {
        mainCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
    }

}
