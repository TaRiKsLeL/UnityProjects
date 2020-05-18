using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class GameManager : MonoBehaviour
{
    public int level = 1;
    public int hp = 3;
    public int score = 0;
    public float counter = 0;

    [SerializeField] GameObject pacmanPrefab;
    [SerializeField] GameObject dotPrefab;
    [SerializeField] GameObject wallPrefab;

    [SerializeField] NavMeshSurface surface;

    [Header("Enemy")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float enemyMinSpeed=1f;
    [SerializeField] float enemyMaxSpeed=5f;
    [SerializeField] List<Material> enemyMaterials;

    private Transform levelTransform;
    private Vector3 pacmanPosition;

    GameState gameState;

    private int randMapIndex=0;

    private List<int[,]> arrayList = new List<int[,]>
    {
         new int[,]{
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
         },
         new int[,]{
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
        {1,0,0,0,0,1,0,0,0,0,0,0,0,0,1 },
        {1,0,1,1,0,1,1,0,1,1,1,1,1,0,1 },
        {1,0,1,0,0,0,0,0,1,0,0,0,1,0,1 },
        {1,0,1,1,1,0,1,1,1,0,1,1,1,0,1 },
        {1,0,1,0,0,0,0,0,1,0,0,0,1,0,1 },
        {1,0,0,0,0,0,1,1,1,0,0,0,1,0,1 },
        {1,0,1,0,0,0,0,0,0,0,1,0,0,0,1 },
        {1,0,0,0,1,0,1,0,1,1,1,1,1,0,1 },
        {1,0,0,0,1,0,0,0,0,0,1,0,0,0,1 },
        {1,1,1,1,1,0,1,1,1,1,1,0,1,1,1 },
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
        {1,0,1,1,1,1,1,1,1,0,1,1,1,1,1 },
        {1,0,0,0,1,0,0,0,0,0,0,0,0,0,1 },
        {1,1,1,1,1,1,1,0,1,1,1,1,1,1,1 }
         }
    };

    void Start()
    {
        GenerateLevel();
    }


    void Update()
    {
        counter += Time.deltaTime;
    }

    public void GenerateLevel()
    {
        Camera.main.backgroundColor = GetRandColor();


        levelTransform = new GameObject("Level").transform;

        randMapIndex = UnityEngine.Random.Range(0, arrayList.Count);
        GameObject.FindWithTag("Ground").transform.localScale = new Vector3(arrayList[randMapIndex].GetLength(0), 1, arrayList[randMapIndex].GetLength(1));

        GenerateDotsAndWalls();
        surface.BuildNavMesh();

        var pacmanPos = GetRandMapPosition(arrayList[randMapIndex]);
        GameObject toInstanctiate = Instantiate(pacmanPrefab, new Vector3(pacmanPos.x + 0.5f, 0.5f, pacmanPos.y + 0.5f), Quaternion.identity);
        pacmanPosition = toInstanctiate.transform.position;
        toInstanctiate.transform.SetParent(levelTransform);

        GenerateEnemies(level);
    }

    private void GenerateDotsAndWalls()
    {
        Transform dots = new GameObject("Dots").transform;
        Transform walls = new GameObject("Walls").transform;

        for(int i = 0; i < arrayList[randMapIndex].GetLength(0); i++)
        {
            for(int j = 0; j < arrayList[randMapIndex].GetLength(1); j++)
            {
                if (arrayList[randMapIndex][i, j] == 0)
                {
                    if (pacmanPosition.x == i + 0.5f && pacmanPosition.z == j + 0.5f)
                        continue;
                    GameObject toInstanctiate = Instantiate(dotPrefab, new Vector3(i + 0.5f, dotPrefab.transform.position.y, j + 0.5f), Quaternion.identity);
                    toInstanctiate.transform.SetParent(dots);
                }else if(arrayList[randMapIndex][i, j] == 1)
                {
                    GameObject toInstanctiate = Instantiate(wallPrefab, new Vector3(i + 0.5f, wallPrefab.transform.position.y, j + 0.5f), Quaternion.identity);
                    toInstanctiate.transform.SetParent(walls);
                }
            }
        }

        dots.SetParent(levelTransform);
        walls.SetParent(levelTransform);
    }

    private void GenerateEnemies(int amount)
    {
        Transform enemies = new GameObject("Enemies").transform;
        for (int i = 0; i < amount; i++)
        {
            var pos = GetRandMapPosition(arrayList[randMapIndex]);

            GameObject toInstanctiate = Instantiate(enemyPrefab, new Vector3(pos.x + 0.5f, 0.5f, pos.y + 0.5f), Quaternion.identity);
            toInstanctiate.GetComponent<NavMeshAgent>().speed = UnityEngine.Random.Range(enemyMinSpeed, enemyMaxSpeed + 1);
            toInstanctiate.GetComponent<Renderer>().material = enemyMaterials[UnityEngine.Random.Range(0, enemyMaterials.Count)];   
            toInstanctiate.transform.SetParent(enemies);
        }
        enemies.SetParent(levelTransform);
    }

    private Vector2 GetRandMapPosition(int[,] map)
    {
        int x = 0;
        int y = 0;
        do{
            x = UnityEngine.Random.Range(0, map.GetLength(0));
            y = UnityEngine.Random.Range(0, map.GetLength(1));
        } while (map[x,y]==1);

        Vector2 pos = new Vector2(x, y);
        
        return pos;
    }

    private Color GetRandColor()
    {
        return new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
    }

    public int[,] GetMap()
    {
        return arrayList[randMapIndex];
    }

}

enum GameState
{
    BEFORE_FIRST_CLICK,
    PLAYING,
    PAUSE,
    GAME_OVER,
    MAX_STATES_AMOUNT
}