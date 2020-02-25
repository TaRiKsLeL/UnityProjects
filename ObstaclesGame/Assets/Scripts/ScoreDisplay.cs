using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScoreDisplay : MonoBehaviour
{
    Player player;
    Text text;

    void Start()
    {
        player = FindObjectOfType<Player>();
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = "HP: "+Convert.ToInt32(player.GetHealth()).ToString();
    }
}
