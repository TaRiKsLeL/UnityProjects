using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountryScript : MonoBehaviour
{
    float moveSpeed = 0;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = Random.Range(0.25f,1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, -4, transform.position.z);
        var movementThisFrame = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementThisFrame);
    }

    private void OnMouseUpAsButton()
    {
        print(FindObjectOfType<CCUIDisplay>().GetCapitalText().text + "   " + GetComponent<CounrtyInfo>().GetCapital());
        if (FindObjectOfType<CCUIDisplay>().GetCapitalText().text.Equals(GetComponent<CounrtyInfo>().GetCapital()))
        {
            print("Destroy " + GetComponent<CounrtyInfo>().GetCapital());
            Destroy(gameObject);
            FindObjectOfType<CountryController>().lastCapital = GetComponent<CounrtyInfo>().GetCapital();
            FindObjectOfType<CCUIDisplay>().SetCapitalText(FindObjectOfType<CountryController>().GetRandomCountryOnScreen().GetComponent<CounrtyInfo>().GetCapital());
            FindObjectOfType<MiniGameSession>().AddToScore(1);
            
        }
    }

}
