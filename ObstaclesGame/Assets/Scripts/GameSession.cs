using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    [Header("Level")]
    [SerializeField] int increaseLevelGap = 5;
    [SerializeField] float fallingSpeed=1f;
    [SerializeField] float boostValue = 5f;
    
    [Header("Background")]
    [SerializeField] GameObject background;
    [SerializeField] GameObject trees;
    [SerializeField] public float scrollSpeed = 0.5f;

    private float timer = 0f;
    private ObstaclesGenerator obstaclesGenerator;
    private int savedSec = 0;
    private bool isBoosting = false;

    void Start()
    {
        SetupSingleton();
        obstaclesGenerator = FindObjectOfType<ObstaclesGenerator>();
        ChangeBackAndTreesSpeed(scrollSpeed);
    }

    private void SetupSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0.0f)
        {
            timer += Time.deltaTime;
        }

        if (Convert.ToInt32(timer) % increaseLevelGap == 0 && savedSec< Convert.ToInt32(timer))
        {
            fallingSpeed += 0.1f;
            scrollSpeed += 0.00725f;
            ChangeBackAndTreesSpeed(scrollSpeed);
            obstaclesGenerator.AddToObstaclesAmount(1);

            savedSec = Convert.ToInt32(timer);
        }

    }

    public float GetFallingSpeed()
    {
        return fallingSpeed;
    }

    public void SetFallingSpeed(float speed)
    {
        fallingSpeed = speed;
    }

    public void ChangeBackAndTreesSpeed(float speed)
    {
        background.GetComponent<BackgroundScroller>().ChangeScrollSpeed(speed);
        trees.GetComponent<BackgroundScroller>().ChangeScrollSpeed(speed);
    }

    public void Boost()
    {
        isBoosting = true;
        fallingSpeed *= boostValue;
        scrollSpeed *= boostValue;
        ChangeBackAndTreesSpeed(scrollSpeed);
    }

    public void StopBoost()
    {
        isBoosting = false;
        fallingSpeed /= boostValue;
        scrollSpeed /= boostValue;
        ChangeBackAndTreesSpeed(scrollSpeed);
    }

    public float GetBoostDelta()
    {
        if (isBoosting)
        {
            return fallingSpeed / boostValue;
        }
        return 1;
    }

    public int GetLevelGap()
    {
        return increaseLevelGap;
    }

    public float GetTimer()
    {
        return timer;
    }
}
