using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverDown : MonoBehaviour
{

    private float movingSpeed=-1f;

    void Start()
    {
        
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
            movementThisFrame = movingSpeed * Time.deltaTime;
        }
        else
        {
            movementThisFrame = FindObjectOfType<GameSession>().GetFallingSpeed() * Time.deltaTime;
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
