using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    public Texture theTexture;
    private float startTime;
    
    // Start is called before the first frame update
   public void OnLevelWasLoaded(int level)
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time -startTime >= 5)
        {
            Destroy(gameObject);
        }
    }

    private void OnGUI()
    {
        Color fadeColor = GUI.color;
        GUI.color = Color.white;
        fadeColor.a = Mathf.Lerp(1.0f, 0.0f, (Time.time - startTime));
        GUI.color = fadeColor;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), theTexture);
    }
}
