using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int level = 1;

    private MazeSpawner mazeSpawner;
    private UIManager uiManager;

    private void Start()
    {
        mazeSpawner = FindObjectOfType<MazeSpawner>();
        uiManager = FindObjectOfType<UIManager>();
        uiManager.SetLevelText(level);
        uiManager.SetMapSizeText(mazeSpawner.width, mazeSpawner.height);
    }

    public void OnFinish()
    {
        level++;
        mazeSpawner.width++;
        mazeSpawner.height++;
        mazeSpawner.CleanMaze();
        mazeSpawner.SpawnMaze();

        uiManager.SetLevelText(level);
        uiManager.SetMapSizeText(mazeSpawner.width, mazeSpawner.height);

    }


}
