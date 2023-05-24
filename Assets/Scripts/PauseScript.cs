using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseScreen;

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    public void CheckInput()
    {
        
        if(true)
        {
            if (!pauseScreen.activeSelf)
            {
                pauseScreen.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                pauseScreen.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    public void Resume()
    {
        pauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
