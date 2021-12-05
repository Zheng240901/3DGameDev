using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoosemenu : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {

        SceneManager.LoadScene(sceneName);
    }
    //public void MenuButton()
    //{
    //    SceneManager.LoadScene("Main Menu");
    //}

    //public void StartGame()
    //{
    //   SceneManager.LoadScene("assignment UI");
    //}
}
