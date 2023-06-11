using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadSceneScript : MonoBehaviour
{
    public GameObject resetScreen;

    public void OpenScreen()
    {
        resetScreen.SetActive(true);
    }
    public void ResetScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    private void Start()
    {
        EventManager.instance.onDemoEndReached += OpenScreen;
    }

    private void OnDestroy()
    {
        EventManager.instance.onDemoEndReached -= OpenScreen;

    }
}
