using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsGenerator : MonoBehaviour
{
    [Header("Player Car")]
    [SerializeField] List<GameObject> playerPrefabs;

    [Header("Passing Obstacles")]
    [SerializeField] List<GameObject> obstaclesPrefabs;
    [SerializeField] int maxObstaclesAmount = 5;
    [SerializeField] float speedMin = 1f;
    [SerializeField] float speedMax = 3f;

    [Header("Simple Cars")]
    [SerializeField] List<GameObject> simpleCarsPrefabs;
    [SerializeField] float simpleCarsSpeed = 20.0f;
    [SerializeField] int simpleCarsAmountPerLevel = 2;

    GameSession gameSession;
    float cameraOrtSize;


    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        cameraOrtSize = Camera.main.orthographicSize;

        Instantiate(playerPrefabs[Random.Range(0, playerPrefabs.Count)],new Vector2(2,2),Quaternion.identity);

        StartCoroutine(spawnFlyingObstacles());
    }

    IEnumerator spawnObstacles()
    {
        while (true){
            GameObject obstacle = InastantiateObstacle();
            yield return new WaitForSeconds(cameraOrtSize * 2 / gameSession.GetFallingSpeed() / maxObstaclesAmount);
        }
    }
    private GameObject InastantiateObstacle()
    {
        GameObject obstacle = Instantiate(obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Count)],
            new Vector2(5, Camera.main.orthographicSize * 2 + 1), Quaternion.identity);

        float startP = 0.7f;
        float endP = 4.8f;
        if (Random.value > 0.5)
        {
            Foo.Swap<float>(ref startP, ref endP);
        }

        obstacle.GetComponent<Obstacle>().SetStartPoint(startP);
        obstacle.GetComponent<Obstacle>().SetEndPoint(endP);
        obstacle.GetComponent<Obstacle>().SetSpeed(Random.Range(speedMin, speedMax));
        return obstacle;
    }

    IEnumerator spawnFlyingObstacles()
    {
        while (true)
        {
            GameObject obstacle = Instantiate(simpleCarsPrefabs[Random.Range(0, simpleCarsPrefabs.Count)],
            new Vector2(Random.Range(2, (float)(cameraOrtSize * 2.0) * Screen.width / Screen.height)-1, cameraOrtSize * 2 + 1), Quaternion.identity);
            obstacle.GetComponent<MoverDown>().SetSpeed(simpleCarsSpeed);
            yield return new WaitForSeconds(gameSession.GetLevelGap()/ simpleCarsAmountPerLevel);
        }

    }
    public void AddToObstaclesAmount(int num)
    {
        maxObstaclesAmount += num;
    }
}
