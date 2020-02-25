using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] GameObject tile;

    [HideInInspector] public GameManager gameManager;

    GameObject[,] itemsBoard;
    Transform boardHolder;

    public void Awake()
    {
        itemsBoard = new GameObject[4,4];
    }

    public void SetupScene()
    {
        boardHolder = new GameObject("Board").transform;

        for(int x = 0; x < gameManager.columns; x++)
        {
            for(int y = 0; y < gameManager.rows; y++)
            {
                if (x == gameManager.columns - 1 && y == 0) continue;

                GameObject toInstansiate = Instantiate(tile, new Vector2(x, y), Quaternion.identity);
                toInstansiate.GetComponent<Item>().number = GetUniqueItemNumber();
                toInstansiate.GetComponent<SpriteRenderer>().color = GetRandColor();
                toInstansiate.transform.SetParent(boardHolder);
                itemsBoard[x, y] = toInstansiate;
            }
        }
    }

    private int GetUniqueItemNumber()
    {
        int number=0;
        do
        {
            number = Random.Range(1, (gameManager.columns * gameManager.rows));

        } while (isOnBoard(number));

        return number;
    }

    private bool isOnBoard(int number)
    {
        for (int x = 0; x < gameManager.columns; x++)
        {
            for (int y = 0; y < gameManager.rows; y++)
            {
                if (itemsBoard[x, y]) {
                    Item i = itemsBoard[x, y].GetComponent<Item>();
                    if (i.number == number) return true;
                }
            }
        }

        return false;
    }

    private Color GetRandColor()
    {
        return new Color(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), 1f);
    }

    public bool CanMove(Item item, out Direction direction)
    {
        for (int x = 0; x < gameManager.columns; x++)
        {
            for (int y = 0; y < gameManager.rows; y++)
            {
                if (!itemsBoard[x, y]) continue;
                if (itemsBoard[x, y].GetComponent<Item>().number == item.number)
                {
                    if (isEmptyItem(x - 1, y))
                    {
                        direction = Direction.LEFT;
                        return true;
                    }
                    else if (isEmptyItem(x + 1, y))
                    {
                        print("TO RIGHT");
                        direction = Direction.RIGHT;
                        return true;
                    }
                    else if (isEmptyItem(x, y - 1))
                    {
                        direction = Direction.DOWN;
                        return true;
                    }
                    else if (isEmptyItem(x, y + 1)) 
                    {
                        direction = Direction.UP;
                        return true;
                    }

                }
            }
        }
        direction = Direction.UP;
        return false;
    }

    private bool isEmptyItem(int x,int y)
    {
        try
        {
            if (!itemsBoard[x, y])
            {
                return true;
            }
        }
        catch
        {
            return false;
        }
        return false;
    }

    public void SwapItems(Item item)
    {
        IntPoint nullItem = new IntPoint();
        IntPoint swapItem = new IntPoint();

        for (int x = 0; x < gameManager.columns; x++)
        {
            for (int y = 0; y < gameManager.rows; y++)
            {
                if (!itemsBoard[x, y])
                {
                    nullItem.x = x;
                    nullItem.y = y;
                    continue;
                }
                if (itemsBoard[x, y].GetComponent<Item>().number == item.number)
                {
                    swapItem.x = x;
                    swapItem.y = y;
                }
            }
        }
        Foo.Swap<GameObject>(ref itemsBoard[nullItem.x, nullItem.y], ref itemsBoard[swapItem.x, swapItem.y]);
    }

    // Update is called once per frame
    void Update()
    {
        
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

public struct IntPoint
{
    public int x, y;
}

public enum Direction
{
    UP,
    RIGHT,
    DOWN,
    LEFT
}