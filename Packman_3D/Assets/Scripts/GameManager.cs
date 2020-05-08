using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pacman;
    [SerializeField] GameObject dot;
    [SerializeField] GameObject wallPrefab;

    [Header("Pacman Position")]
    [SerializeField] float x = 1;
    [SerializeField] float z = 1;

    Vector3 groundScale;
    GameState gameState;

    private int[,] map =
    {
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
        {1,0,1,1,1,1,1,0,1,1,1,1,1,0,1 },
        {1,0,1,0,0,0,0,0,1,0,0,0,1,0,1 },
        {1,0,1,1,1,0,1,1,1,0,1,1,1,0,1 },
        {1,0,1,0,0,0,0,0,1,0,0,0,1,0,1 },
        {1,0,1,0,0,0,1,1,1,0,0,0,1,0,1 },
        {1,0,0,0,1,0,0,0,0,0,1,0,0,0,1 },
        {1,0,1,1,1,1,1,0,1,1,1,1,1,0,1 },
        {1,0,0,0,1,0,0,0,0,0,1,0,0,0,1 },
        {1,1,1,0,1,0,1,1,1,0,1,0,1,1,1 },
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
        {1,0,1,1,1,1,1,0,1,1,1,1,1,0,1 },
        {1,0,0,0,1,0,0,0,0,0,1,0,0,0,1 },
        {1,1,1,1,1,1,1,0,1,1,1,1,1,1,1 }
    };
    MapMarks[,] mapMarks;

    // Start is called before the first frame update
    void Start()
    {
        groundScale = GameObject.FindWithTag("Ground").transform.localScale;
        pacman = Instantiate(pacman, new Vector3(x+0.5f, 0.5f, z + 0.5f), Quaternion.identity);
        GenerateDotsAndWalls();
        gameState = GameState.BEFORE_FIRST_CLICK;
        //InitMapArray();
    }

    private void GenerateDotsAndWalls()
    {
        Transform dots = new GameObject("Dots").transform;
        Transform walls = new GameObject("Walls").transform;

        for(int i = 0; i < groundScale.x; i++)
        {
            for(int j = 0; j < groundScale.z; j++)
            {
                if (map[i, j] == 0)
                {
                    if (pacman.transform.position.x == i + 0.5f && pacman.transform.position.z == j + 0.5f)
                        continue;
                    GameObject toInstanctiate = Instantiate(dot, new Vector3(i + 0.5f, dot.transform.position.y, j + 0.5f), Quaternion.identity);
                    toInstanctiate.transform.SetParent(dots);
                }else if(map[i, j] == 1)
                {
                    GameObject toInstanctiate = Instantiate(wallPrefab, new Vector3(i + 0.5f, wallPrefab.transform.position.y, j + 0.5f), Quaternion.identity);
                    toInstanctiate.transform.SetParent(walls);
                }
            }
        }
    }

    private void InitMapArray()
    {
        map = new int[(int)groundScale.x, (int)groundScale.z];
        mapMarks = new MapMarks[(int)groundScale.x, (int)groundScale.z];

        print(mapMarks.GetLength(0)+"   "+ mapMarks.GetLength(1));
        int k = 0;

        for (int i = 0; i < mapMarks.GetLength(0); i++)
        {
            for (int j = 0; j < mapMarks.GetLength(1); j++)
            {
                k++;
                if (Physics.OverlapSphere(new Vector3(i + 0.5f, 0.5f, j + 0.5f), 0,9).Length<=0)
                {
                    mapMarks[i, j] = MapMarks.EMPTY;
                }
                else
                {
                    Instantiate(pacman, new Vector3(i + 0.5f, 0.8f, j + 0.5f), Quaternion.identity);
                    mapMarks[i, j] = MapMarks.WALL;
                }
                print(k+"   "+Enum.GetName(typeof(MapMarks), mapMarks[i, j]));
            }
        }

    }

    public int[,] GetMap()
    {
        return map;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

enum MapMarks
{
    EMPTY,
    WALL
}

enum GameState
{
    BEFORE_FIRST_CLICK,
    PLAYING,
    PAUSE,
    GAME_OVER,
    MAX_STATES_AMOUNT
}