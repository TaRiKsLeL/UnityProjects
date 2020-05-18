using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverDown : MonoBehaviour
{
    GameSession gameSession;
    private float movingSpeed=-1f;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    void Update()
    {
        MoveDown();
    }

    private void MoveDown()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, -7, transform.position.z);
        float movementThisFrame;

        if (movingSpeed!=-1)
        {
            movementThisFrame = movingSpeed * gameSession.GetBoostDelta() * Time.deltaTime;
        }
        else
        {
            movementThisFrame = gameSession.GetFallingSpeed() * Time.deltaTime;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementThisFrame);
    }

    public void SetSpeed(float speed)
    {
        movingSpeed = speed;
    }

    public float GetSpeed()
    {
        return movingSpeed;
    }
}
