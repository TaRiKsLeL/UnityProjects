using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUIManager : MonoBehaviour
{
    public Text timeToGetText;
    public GameObject nextBtn;

    public Text rightAttemptsText;
    public Text timerText;

    public GameObject mainCanvas;
    public GameObject gameOverCanvas;

    private ClockGameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<ClockGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        rightAttemptsText.text = "Рахунок: " + gameManager.rightAttempts.ToString();
        timerText.text = "Час: " + ((int)gameManager.timerVal).ToString();
    }

    public void SetTimeText(MyTime time)
    {
        timeToGetText.text = time.Hour.ToString("00");
        timeToGetText.text += ":"+time.Minute.ToString("00");
    }

    public void SetNextButtonActive(bool state) 
    {
        nextBtn.SetActive(state);
    }

    public void GameOver()
    {
        mainCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
    }
}
