using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI mapSizeText;
    [SerializeField] TextMeshProUGUI timerText;

    public void SetTimerText(float num)
    {
        timerText.text = ((int)num).ToString();
    }

    public void SetLevelText(int level)
    {
        levelText.text = "РІВЕНЬ " +level;
    }

    public void SetMapSizeText(int w, int h)
    {
        mapSizeText.text = w+"x"+h;
    }
}
