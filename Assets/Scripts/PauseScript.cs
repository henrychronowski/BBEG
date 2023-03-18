using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(Input.GetKeyDown(KeyCode.P))
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
