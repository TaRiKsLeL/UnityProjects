using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CCUIDisplay : MonoBehaviour
{
    [SerializeField] Text capitalText;

    // Start is called before the first frame update
    void Start()
    {
        //if (FindObjectOfType<CountryController>() != null)
        //{
        //    capitalText.text = FindObjectOfType<CountryController>().GetRandomCountryOnScreen().GetComponent<CounrtyInfo>().getCapital();
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Text GetCapitalText()
    {
        return capitalText;
    }

    public void SetCapitalText(string text)
    {
        capitalText.text = text;
    }
}
