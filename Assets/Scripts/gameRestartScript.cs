using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class gameRestartScript : MonoBehaviour
{
    public void resetTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
