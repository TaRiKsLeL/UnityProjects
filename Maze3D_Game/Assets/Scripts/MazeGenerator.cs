using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneratorCell
{
    public int x, y;

    public bool wallButtom=true, wallLeft=true;

    public bool visited = false;
    public int distanceFromStart;
}

public class MazeGenerator
{
    public int width = 4;
    public int height = 4;

    MazeGeneratorCell[,] maze;

    public MazeGenerator(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public MazeGeneratorCell[,] GenerateMaze()
    {
        maze = new MazeGeneratorCell[width, height];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                maze[x, y] = new MazeGeneratorCell { x = x, y = y };
            }
        }

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            maze[x, height - 1].wallLeft = false;
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            maze[width - 1, y].wallButtom = false;
        }

        RemoveWallsWithBacktracker();

        return maze;
    }


    private void RemoveWallsWithBacktracker()
    {
        MazeGeneratorCell currentCell = maze[0, 0]; // Починаємо з початку
        currentCell.visited = true;
        currentCell.distanceFromStart = 0;

        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();
        do
        {
            List<MazeGeneratorCell> unvisitedNeighbours = new List<MazeGeneratorCell>();
            fillUnvisitedNeigh(currentCell, unvisitedNeighbours);

            if (unvisitedNeighbours.Count > 0)
            {
                MazeGeneratorCell chosen = unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
                RemoveWall(currentCell, chosen);

                chosen.visited = true;
                stack.Push(chosen);
                currentCell = chosen;
                currentCell.distanceFromStart = stack.Count;
            }
            else
            {
                currentCell = stack.Pop();
            }

        } while (stack.Count > 0);
    }

    private void RemoveWall(MazeGeneratorCell cur, MazeGeneratorCell chos)
    {
        if (cur.x == chos.x)
        {
            if (cur.y > chos.y)
            {
                cur.wallButtom = false;
            }
            else
            {
                chos.wallButtom = false;
            }
        }
        else
        {
            if (cur.x > chos.x)
            {
                cur.wallLeft = false;
            }
            else
            {
                chos.wallLeft = false;
            }
        }
    }

    private void fillUnvisitedNeigh( MazeGeneratorCell currentCell, List<MazeGeneratorCell> unvisitedNeighbours)
    {
        int x = currentCell.x;
        int y = currentCell.y;

        if (x > 0 && !maze[x - 1, y].visited)
        {
            unvisitedNeighbours.Add(maze[x - 1, y]);
        }

        if (y > 0 && !maze[x, y - 1].visited)
        {
            unvisitedNeighbours.Add(maze[x, y - 1]);
        }

        if (x < width - 2 && !maze[x + 1, y].visited)
        {
            unvisitedNeighbours.Add(maze[x + 1, y]);
        }
        if (y < height - 2 && !maze[x, y + 1].visited)
        {
            unvisitedNeighbours.Add(maze[x, y + 1]);
        }
    }

    public MazeGeneratorCell GetMazeExit()
    {
        MazeGeneratorCell furthest = maze[0,0];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            if (maze[x, height - 2].distanceFromStart > furthest.distanceFromStart) furthest = maze[x, height - 2];
            if (maze[x, 0].distanceFromStart > furthest.distanceFromStart) furthest = maze[x, 0];
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            if (maze[width - 2, y].distanceFromStart > furthest.distanceFromStart) furthest = maze[width - 2, y];
            if (maze[0, y].distanceFromStart > furthest.distanceFromStart) furthest = maze[0, y];
        }

        return furthest;
    }
}
