using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultGameManager : MonoBehaviour
{
    public int rightAttempts=0;
    public int falseAttempts=0;
    public float timerVal = 60;

    public int firstNum = 1;
    public int secondNum = 1;
    public int result=0;
    public int guessedResult = 0;

    public InputField resultInputField;

    private MultUIManager uIManager;

    // Start is called before the first frame update
    void Start()
    {
        uIManager = FindObjectOfType<MultUIManager>();
        GenerateNewTask();
    }

    // Update is called once per frame
    void Update()
    {
        timerVal -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SubmitResult();
            resultInputField.text = "";
            resultInputField.Select();
            resultInputField.ActivateInputField();
        }

        if (timerVal <= 0)
        {
            uIManager.GameOver();
        }
    }

    private void GenerateNewTask()
    {
        firstNum = Random.Range(0, 11);
        secondNum = Random.Range(0, 11);
        result = firstNum * secondNum;
    }

    private void SubmitResult()
    {
        guessedResult = System.Convert.ToInt32(resultInputField.text);

        if (guessedResult == result){
            rightAttempts++;
        }
        else{
            falseAttempts++;
        }

        GenerateNewTask();
    }
}
