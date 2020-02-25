using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedsGenerator : MonoBehaviour
{
    [Header("Projectile Seeds")]
    [SerializeField] GameObject projectileSeedPrefab;
    [SerializeField] float projectileSeedsAmountPerLevel = 1f;

    [Header("Health Seeds")]
    [SerializeField] GameObject healthSeedPrefab;
    [SerializeField] float healthSeedsAmountPerLevel = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateProjectileSeeds());
        StartCoroutine(GenerateHealthSeeds());
    }

    IEnumerator GenerateProjectileSeeds()
    {
        while (true)
        {
            GameObject seed = Instantiate(projectileSeedPrefab,
                new Vector2(Random.Range(0, (float)(Camera.main.orthographicSize * 2.0) * Screen.width / Screen.height),
                Camera.main.orthographicSize * 2 + 1), Quaternion.identity);
            yield return new WaitForSeconds(FindObjectOfType<GameSession>().GetLevelGap() / projectileSeedsAmountPerLevel);
        }
    }
    
    IEnumerator GenerateHealthSeeds()
    {
        while (true)
        {
            GameObject seed = Instantiate(healthSeedPrefab,
                new Vector2(Random.Range(0, (float)(Camera.main.orthographicSize * 2.0) * Screen.width / Screen.height),
                Camera.main.orthographicSize * 2 + 1), Quaternion.identity);
            yield return new WaitForSeconds(FindObjectOfType<GameSession>().GetLevelGap() / healthSeedsAmountPerLevel);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
