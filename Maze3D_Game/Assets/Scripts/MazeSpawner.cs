using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEditor.UIElements;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject cellPrefab;
    [SerializeField] GameObject finishPrefab;
    public int width=4;
    public int height=4;

    private MazeGenerator mazeGenerator;

    private float delta = 10f;
    private Transform mazeHolder;

    void Start()
    {
        SpawnMaze();
    }

    public void SpawnMaze()
    {
        mazeGenerator = new MazeGenerator(width, height);
        MazeGeneratorCell[,] maze = mazeGenerator.GenerateMaze();

        mazeHolder = new GameObject("Maze Spawned").transform;
        var player = Instantiate(playerPrefab, new Vector3(1 * delta / 2, 1, 1 * delta / 2), Quaternion.identity);
        player.transform.SetParent(mazeHolder);

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                var toInst = Instantiate(cellPrefab, new Vector3(x * delta, 0, y * delta), Quaternion.identity);
                toInst.transform.SetParent(mazeHolder);

                Cell c = toInst.GetComponent<Cell>();
                c.wallLeft.SetActive(maze[x, y].wallLeft);
                c.wallButtom.SetActive(maze[x, y].wallButtom);
            }
        }

        SetFinishCell();
    }

    private void SetFinishCell()
    {
        MazeGeneratorCell finish = mazeGenerator.GetMazeExit();
        print(finish.x + " " + finish.y);
        float x = finish.x * delta + (delta / 2);
        float y = finish.y * delta + (delta / 2);
        Instantiate(finishPrefab, new Vector3(x, 0.5f, y), Quaternion.identity).transform.SetParent(mazeHolder);
    }

    public void CleanMaze()
    {
        Destroy(GameObject.Find("Maze Spawned"));
    }
}
