﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    void Awake()
    {
        if(GameManager.instance==null)
        {
            Instantiate(gameManager);
        }
    }
}
