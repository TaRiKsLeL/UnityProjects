using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SeedsDisplay : MonoBehaviour
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
        text.text = "x" + Convert.ToInt32(player.GetSeeds()).ToString();
    }
}
