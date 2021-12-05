using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoose : MonoBehaviour
{
    private bool gameEnded;
    //public GameObject CanvasWin;
    //public GameObject CanvasLose;
    //public AudioSource audioSource;
    //public AudioSource looseaudioSource;

    public void WinLevel()
    {
        if (!gameEnded)
        {
            Debug.Log("You Win!");
            //CanvasWin.SetActive(true);
            gameEnded = true;
            //audioSource.Play();
            SceneManager.LoadScene("Game Win");
        }
    }

    public void LooseLevel()
    {
        if (!gameEnded)
        {
            Debug.Log("You Loose!");
            //CanvasLose.SetActive(true);
            gameEnded = true;
            //looseaudioSource.Play();
            SceneManager.LoadScene("Game Over");
        }
    }

}
