using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : Character
{



    protected override void AttemptMove()
    {
        //OnClickMovement();       //1

        FreeMovement();            //2
    }

    private void OnClickMovement()
    {
        SetupDirection();

        print(direction.ToString());

        var transition = speed * Time.deltaTime;

        switch (direction)
        {
            case Direction.UP:
                {
                    transform.Translate(-transition, 0, 0);
                    break;
                }
            case Direction.RIGHT:
                {
                    transform.Translate(0, 0, transition);
                    break;
                }
            case Direction.DOWN:
                {
                    transform.Translate(transition, 0, 0);
                    break;
                }
            case Direction.LEFT:
                {
                    transform.Translate(0, 0, -transition);
                    break;
                }
        }
    }

    private void FreeMovement()
    {
        float translationX = Input.GetAxis("Vertical") * speed;
        float translationZ = Input.GetAxis("Horizontal") * speed;

        translationX *= Time.deltaTime;
        translationZ *= Time.deltaTime;

        transform.Translate(0, 0, translationZ);
        transform.Translate(-translationX, 0, 0);
    }

    private void SetupDirection()
    {
        Vector2 pos = GetPositionInArray();
        int target=0;
        print("Position: "+pos);

        if (Input.GetAxis("Vertical") > 0)
        {
            target = gameMap[(int)pos.x, (int)pos.y + 1];
            if (target != 1)
            {
                direction = Direction.UP;
            }
            else
            {
                direction = Direction.NONE;
            }
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            target = gameMap[(int)pos.x, (int)pos.y - 1];
            if (target != 1)
            {
                direction = Direction.DOWN;
            }
            else
            {
                direction = Direction.NONE;
            }
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            target = gameMap[(int)pos.x+1, (int)pos.y];
            if (target != 1)
            {
                direction = Direction.RIGHT;
            }
            else
            {
                direction = Direction.NONE;
            }
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            target = gameMap[(int)pos.x - 1, (int)pos.y];

            if (target != 1)
            {
                direction = Direction.LEFT;
            }
            else
            {
                direction = Direction.NONE;
            }
        }
        print("Target: "+target);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Dot"))
        {
            SoundsManager.instance.PlaySoundWithoutInterrupt(chomp);
            Destroy(other.gameObject);
        }
    }
}

public enum Direction
{
    NONE,
    UP,
    RIGHT,
    DOWN,
    LEFT,
    MAX_DIRECTIONS
}