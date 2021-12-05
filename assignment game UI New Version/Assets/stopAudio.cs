using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopAudio : MonoBehaviour
{
    public AudioSource sound;
    private Rect audiorect;

    void Start()
    {
        audiorect = new Rect(20, 20, 100, 20);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            sound.Play();
        }
    }

    void OnGUI()
    {
        GUI.Button (audiorect, "Mute");

        if (audiorect.Contains(Event.current.mousePosition))
        {
            if (Input.GetMouseButtonDown(0))
            {
                sound.Pause();
            }
        }
    }
}
