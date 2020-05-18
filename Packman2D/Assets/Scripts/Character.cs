using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    [SerializeField] protected float speed = 10.0f;
    [SerializeField] protected AudioClip chomp;

    protected GameManager gameManager;
    protected int[,] gameMap;
    protected Direction direction;


    protected void Start()
    {
        direction = Direction.NONE;
        gameManager = FindObjectOfType<GameManager>();
        gameMap = gameManager.map;
    }

    protected void Update()
    {
        AttemptMove();
    }

    protected abstract void AttemptMove();

    public Vector2 GetPositionInArray()
    {
        Vector2 vector = new Vector2();

        vector.x = (int)transform.position.z;
        vector.y = (int)transform.position.x;

        return vector;
    }
}
