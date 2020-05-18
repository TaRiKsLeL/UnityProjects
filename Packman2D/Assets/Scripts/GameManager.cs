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

    GameState gameState;

    [HideInInspector] public int[,] map =
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
        pacman = Instantiate(pacman, new Vector3(x+0.5f, 0.5f, z + 0.5f), Quaternion.identity);
        GenerateDotsAndWalls();
        gameState = GameState.BEFORE_FIRST_CLICK;
        //InitMapArray();
    }

    private void GenerateDotsAndWalls()
    {
        Transform dots = new GameObject("Dots").transform;
        Transform walls = new GameObject("Walls").transform;

        for(int i = 0; i <map.GetLength(0); i++)
        {
            for(int j = 0; j < map.GetLength(1); j++)
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