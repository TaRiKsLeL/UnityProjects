using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : Character
{
    protected override void AttemptMove()
    {
        //OnClickMovement();       //1

        //print("Position: " + GetPositionInArray());
        FreeMovement();            //2
    }

    private void OnClickMovement()
    {
        //var position = transform.position;
        //position.x = (int)position.x;
        //position.z = (int)position.z;
        //transform.position = position;

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
        print(gameMap);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Dot"))
        {
            SoundsManager.instance.PlaySoundWithoutInterrupt(chomp);
            Destroy(other.gameObject);
            gameManager.score += 10 * gameManager.level;

            if (GameObject.FindGameObjectsWithTag("Dot").Length == 1)
            {
                gameManager.level++;
                gameManager.hp += 5 * gameManager.level;
                Destroy(GameObject.Find("Level"));
                gameManager.GenerateLevel();
            }
        }

        if (other.gameObject.tag.Equals("Enemy"))
        {
            gameManager.hp--;
            if (gameManager.hp <= 0)
            {
                FindObjectOfType<UIManager>().GameOverTextSetActive(true);
                FindObjectOfType<SceneManagerr>().LoadScene(0, 3);
            }
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