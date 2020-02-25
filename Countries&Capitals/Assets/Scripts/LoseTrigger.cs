using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (FindObjectOfType<CCUIDisplay>().GetCapitalText().text.Equals(other.gameObject.GetComponent<CounrtyInfo>().GetCapital()))
        {
            FindObjectOfType<CCUIDisplay>().SetCapitalText(FindObjectOfType<CountryController>().GetRandomCountryOnScreen().GetComponent<CounrtyInfo>().GetCapital());
        }
        Destroy(other.gameObject);
    }
}
