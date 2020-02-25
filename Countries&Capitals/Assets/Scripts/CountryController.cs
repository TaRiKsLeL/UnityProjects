using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountryController : MonoBehaviour
{
    [SerializeField] List<GameObject> countriesPrefabs;
    [SerializeField] List<int> points;
    
    List<GameObject> countriesOnScreen;
    public string lastCapital;

    // Start is called before the first frame update
    void Start()
    {
        countriesOnScreen = new List<GameObject>();
        InitCountries();
        FindObjectOfType<CCUIDisplay>().SetCapitalText(GetRandomCountryOnScreen().GetComponent<CounrtyInfo>().GetCapital());
    }

    // Update is called once per frame
    void Update()
    {
        CheckCountries();
    }

    private void CheckCountries()
    {
        for (int i = 0; i < countriesOnScreen.Count; i++)
        {
            if (countriesOnScreen[i] == null)
            {
                countriesOnScreen.RemoveAt(i);

                GameObject country = InstantiateCountry(points[i]);
                countriesOnScreen.Insert(i, country);
            }
        }
    }

    private void InitCountries()
    {
        for (int i = 0; i < points.Count; i++)
        {
            GameObject country = InstantiateCountry(points[i]);

            countriesOnScreen.Add(country);
        }
    }

    private GameObject InstantiateCountry(int point)
    {
        Vector2 pos = new Vector2(point, 6);

        GameObject country;

        do
        {
            country = countriesPrefabs[Random.Range(0, countriesPrefabs.Count)];

        } while (IsOnScreen(country));

        return Instantiate(country, pos, Quaternion.identity);
    }

    private bool IsOnScreen(GameObject gameObject)
    {
        for (int i = 0; i < countriesOnScreen.Count; i++)
        {
            if (countriesOnScreen[i].GetComponent<CounrtyInfo>().GetName().
                Equals(gameObject.GetComponent<CounrtyInfo>().GetName()))
            {
                return true;
            }
        }
        return false;
    } 

    public GameObject GetRandomCountryOnScreen()
    {
        GameObject country;

        do
        {
            country = countriesOnScreen[Random.Range(0, countriesOnScreen.Count)];
        } while (country.GetComponent<CounrtyInfo>().GetCapital().Equals(lastCapital));

        return country;
    }

    private int GetCountryIndexByCountry(GameObject gameObject)
    {
        for(int i = 0; i < countriesPrefabs.Count; i++)
        {
            if (gameObject == countriesPrefabs[i])
            {
                return i;
            }
        }

        return -1;
    }
}
