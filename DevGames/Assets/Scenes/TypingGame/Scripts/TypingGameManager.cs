using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingGameManager : MonoBehaviour
{
    public int rightAttempts = 0;
    public float timerVal = 60;

    public List<string> words;

    public string buffer = "";

    void Start()
    {
        GenerateNewWord();
    }

    void Update()
    {
        timerVal -= Time.deltaTime;

        PrecessInput();

        if (timerVal <= 0)
        {
            FindObjectOfType<TypingUIManager>().GameOver();
        }
    }

    private void PrecessInput()
    {
        foreach (char ch in Input.inputString)
        {
            if (ch == buffer[0]){
                buffer = buffer.Remove(0, 1);
            }

            if (buffer.Equals("")){
                GenerateNewWord();
                rightAttempts++;
            }
        }
    }

    private void GenerateNewWord()
    {
        buffer = words[Random.Range(0, words.Count)];
    }
}
