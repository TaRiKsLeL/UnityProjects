using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] Text scoreText;
    [SerializeField] Text hpText;
    [SerializeField] Text levelText;
    [SerializeField] Text timerText;
    [SerializeField] Text gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        scoreText.text ="Score   " + gameManager.score.ToString();
        hpText.text = "HP   " + gameManager.hp.ToString();
        levelText.text = "Level   " + gameManager.level.ToString();
        timerText.text = "Time   " + ((int)gameManager.counter).ToString();
    }

    public void GameOverTextSetActive(bool state)
    {
        gameOverText.gameObject.SetActive(state);
    }
}
