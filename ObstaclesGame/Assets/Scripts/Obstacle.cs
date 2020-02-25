using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Start/End X Points For Passing Obstacle")]
    [SerializeField] float startPoint = 1f;
    [SerializeField] float endPoint = 1.5f;

    [Header("Passing Speed For Passing Obstacle")]
    [SerializeField] float passingSpeed=1.0f;


    float pointToMove;
    bool facingLeft = true;


    void Start()
    {
        if (tag.Equals("Obstacle"))
        {
            transform.position = new Vector2(startPoint, transform.position.y);
            if (endPoint < startPoint)
            {
                Foo.Swap<float>(ref startPoint, ref endPoint);
            }
        }
    }

    void Update()
    {
        if (tag.Equals("Obstacle"))
        {
            MoveSideways();
        }
    }

    private void MoveSideways()
    {

        if (endPoint - transform.position.x < 0.1)
        {
            if(!facingLeft)
            {
                Flip();
            }
            pointToMove = startPoint;
            facingLeft = true;

        }
        else if (transform.position.x - startPoint < 0.1)
        {
            if (facingLeft)
            {
                Flip();
            }
            facingLeft = false;
            pointToMove = endPoint;
        }

        transform.position = Vector2.MoveTowards(transform.position, 
            new Vector2(pointToMove, transform.position.y), passingSpeed * Time.deltaTime);

    }

    private void Flip()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }


    public void SetStartPoint(float point)
    {
        startPoint = point;
    }

    public void SetEndPoint(float point)
    {
        endPoint = point;
    }


    public void SetSpeed(float val)
    {
        passingSpeed = val;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("ProjectileSeedEnvy") && gameObject.tag.Equals("Obstacle")){
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}


public static class Foo
{

    public static void Swap<T>(ref T lhs, ref T rhs)
    {
        T temp = lhs;
        lhs = rhs;
        rhs = temp;
    }
}