using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounrtyInfo : MonoBehaviour
{
    [SerializeField] string countryName;
    [SerializeField] string countryCapital;

    public string GetName()
    {
        return countryName;
    }

    public string GetCapital()
    {
        return countryCapital;
    }
}
