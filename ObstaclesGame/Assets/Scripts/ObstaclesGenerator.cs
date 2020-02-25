using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesGenerator : MonoBehaviour
{
    [Header("Passing Obstacles")]
    [SerializeField] List<GameObject> obstaclesPrefabs;
    [SerializeField] int maxObstaclesAmount = 5;
    [SerializeField] float speedMin = 1f;
    [SerializeField] float speedMax = 3f;

    [Header("Flying Obstacles")]
    [SerializeField] List<GameObject> flyingObstaclesPrefabs;
    [SerializeField] float flyingObstaclesSpeed = 20.0f;
    [SerializeField] int flyingObstaclesAmountPerLevel = 2;

    [Header("Holes")]
    [SerializeField] List<GameObject> holesPrefabs;
    [SerializeField] int holesAmountPerLevel = 2;

    GameSession gameSession;
    float cameraOrtSize;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        cameraOrtSize = Camera.main.orthographicSize;


        StartCoroutine(spawnObstacles());
        StartCoroutine(spawnHoles());
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
            GameObject obstacle = Instantiate(flyingObstaclesPrefabs[Random.Range(0, flyingObstaclesPrefabs.Count)],
            new Vector2(Random.Range(0, (float)(cameraOrtSize * 2.0) * Screen.width / Screen.height), cameraOrtSize * 2 + 1), Quaternion.identity);
            obstacle.GetComponent<MoverDown>().SetSpeed(flyingObstaclesSpeed);
            yield return new WaitForSeconds(gameSession.GetLevelGap()/flyingObstaclesAmountPerLevel);
        }

    }

    IEnumerator spawnHoles()
    {
        while (true)
        {
            GameObject hole = Instantiate(holesPrefabs[Random.Range(0, holesPrefabs.Count)],
            new Vector2(Random.Range(0, (float)(cameraOrtSize * 2.0) * Screen.width / Screen.height), cameraOrtSize * 2 + 1), Quaternion.identity);
            yield return new WaitForSeconds(gameSession.GetLevelGap()/holesAmountPerLevel);
        }

    }
    // Update is called once per frame
    void Update()
    {
        

    }

    public void AddToObstaclesAmount(int num)
    {
        maxObstaclesAmount += num;
    }
}
