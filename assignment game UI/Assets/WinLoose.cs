using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoose : MonoBehaviour
{
    private bool gameEnded;
    public GameObject CanvasWin;
    public GameObject CanvasLose;

    public void WinLevel()
    {
        if (!gameEnded)
        {
            Debug.Log("You Win!");
            CanvasWin.SetActive(true);
            gameEnded = true;
        }
    }

    public void LooseLevel()
    {
        if (!gameEnded)
        {
            Debug.Log("You Loose!");
            CanvasLose.SetActive(true);
            gameEnded = true;
        }
    }

}
