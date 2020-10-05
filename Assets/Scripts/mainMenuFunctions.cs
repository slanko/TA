using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuFunctions : MonoBehaviour
{
    public void startTheGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void quitTheGame()
    {
        Application.Quit();
    }
}
