using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerr : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadNextScene(float time)
    {
        StartCoroutine(LoadNxtScene(time));
    }

    IEnumerator LoadNxtScene(float time)
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadScene(int index, float time)
    {
        StartCoroutine(LoadScn(index, time));
    }

    IEnumerator LoadScn(int index, float time)
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(index);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
