using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseScreen;
    public static PauseScript instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
    }

    public void Pause()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void OnPause()
    {
        Pause();

        if (!pauseScreen.activeSelf)
        {
            pauseScreen.gameObject.SetActive(true);
        }
        else
        {
            pauseScreen.gameObject.SetActive(false);
        }
    }

    public void Resume()
    {
        Pause();

        pauseScreen.gameObject.SetActive(false);
    }
}
