using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator
{

    private static Generator instance;

    private Generator() { }

    public static Generator getInstance()
    {
        if (instance == null)
        {
            instance = new Generator();
        }
        return instance;
    }

    public Vector2 GenerateRandPosition()
    {
        return new Vector2(Random.Range(0f, (float)(Camera.main.orthographicSize * 2.0) * Screen.width / Screen.height), Random.Range(0f, Camera.main.orthographicSize * 2));
    }

    public Color GenerateRandColor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
    }
}
