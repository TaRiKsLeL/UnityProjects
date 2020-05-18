using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsGenerator : MonoBehaviour
{
    [Header("Player Car")]
    [SerializeField] List<GameObject> playerPrefabs;
   
    [Header("Simple Cars")]
    [SerializeField] List<GameObject> simpleCarsPrefabs;
    [SerializeField] float speedMin = 1f;
    [SerializeField] float speedMax = 3f;
    [SerializeField] int carsAmount = 2;

    GameSession gameSession;
    float cameraOrtSize;


    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        cameraOrtSize = Camera.main.orthographicSize;

        Instantiate(playerPrefabs[Random.Range(0, playerPrefabs.Count)],new Vector2(3,2),Quaternion.identity);

        StartCoroutine(spawnSimpleCars());
    }


    IEnumerator spawnSimpleCars()
    {
        while (true)
        {
            GameObject obstacle = Instantiate(simpleCarsPrefabs[Random.Range(0, simpleCarsPrefabs.Count)],
            new Vector2(Random.Range(2, (float)(cameraOrtSize * 2.0) * Screen.width / Screen.height)-1, cameraOrtSize * 2 + 1), Quaternion.Euler(0,0,180f));
            obstacle.GetComponent<MoverDown>().SetSpeed(gameSession.GetFallingSpeed()+Random.Range(speedMin, speedMax));

            float gap = gameSession.GetLevelGap() / carsAmount * gameSession.GetBoostDelta();

            yield return new WaitForSeconds(gap);
        }

    }
    public void AddToCarsAmount(int num)
    {
        carsAmount += num;
    }
}
