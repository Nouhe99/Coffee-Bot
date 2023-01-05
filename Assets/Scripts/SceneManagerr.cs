using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerr : MonoBehaviour
{
    public GameObject Panel;
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void HowToPlay()
    {
        Panel.SetActive(true);
    }

    public void HowToPlayClose()
    {
        Panel.SetActive(false);

    }
}
