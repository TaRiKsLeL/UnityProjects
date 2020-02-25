using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int columns = 4;
    public int rows = 4;

    [HideInInspector] public static GameManager instance;
    [HideInInspector] public List<GameObject> tiles;

    BoardController boardGenerator;

    void Start()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
        DontDestroyOnLoad(gameObject);


        boardGenerator = GetComponent<BoardController>();
        boardGenerator.gameManager = this;
        InitGame();
    }

    private void InitGame()
    {
        tiles.Clear();
        boardGenerator.SetupScene();

    }

    void Update()
    {
        
    }
}
