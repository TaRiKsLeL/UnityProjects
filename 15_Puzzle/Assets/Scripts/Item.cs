using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    [SerializeField] GameObject numberText;
    [HideInInspector] public int number = 0;
    [SerializeField] float moveSpeed=1f;

    Direction directionToMove;

    void Start()
    {
        SetupNumberText();
    }

    private void SetupNumberText()
    {
        numberText = Instantiate(numberText, transform.position, Quaternion.identity);
        numberText.GetComponent<Text>().text = number.ToString();
        numberText.transform.SetParent(GameObject.FindWithTag("Canvas").transform);
        numberText.transform.localScale = new Vector3(1, 1, 1);
    }

    void Update()
    {
        numberText.transform.position = transform.position;
    }

    private void OnMouseUp()
    {
        if (FindObjectOfType<BoardController>().CanMove(this, out directionToMove))
        {
            FindObjectOfType<BoardController>().SwapItems(this);
            Move(directionToMove);
            print("CAN MOVE");
        }
    }

    private void Move(Direction directionToMove)
    {
        switch (directionToMove)
        {
            case Direction.UP:
                {
                    transform.position = new Vector2(transform.position.x, transform.position.y + 1);
                    break;
                }
            case Direction.RIGHT:
                {
                    transform.position = new Vector2(transform.position.x + 1, transform.position.y);
                    break;
                }
            case Direction.DOWN:
                {
                    transform.position = new Vector2(transform.position.x, transform.position.y - 1);
                    break;
                }
            case Direction.LEFT:
                {
                    transform.position = new Vector2(transform.position.x - 1, transform.position.y);
                    break;
                }
        }
    }

    private IEnumerator SmoothMovement(Direction directionToMove)
    {
        Vector3 end = transform.position;
        switch (directionToMove)
        {
            case Direction.UP:
                {
                    end = new Vector2(transform.position.x, transform.position.y + 1);
                    break;
                }
            case Direction.RIGHT:
                {
                    end = new Vector2(transform.position.x+1, transform.position.y);
                    break;
                }
            case Direction.DOWN:
                {
                    end = new Vector2(transform.position.x, transform.position.y - 1);
                    break;
                }
            case Direction.LEFT:
                {
                    end = new Vector2(transform.position.x-1, transform.position.y);
                    break;
                }
        }
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPositing = Vector3.MoveTowards(transform.position, end, moveSpeed * Time.deltaTime);
            //rb2D.MovePosition(newPositing);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
    }
}

