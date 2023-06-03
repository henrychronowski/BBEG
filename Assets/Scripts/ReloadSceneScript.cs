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
}
