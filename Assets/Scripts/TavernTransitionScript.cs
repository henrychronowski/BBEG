using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TavernTransitionScript : MonoBehaviour
{
    public string sceneName;
    public LoadingScript loading;

    private void Start()
    {
        loading = GameObject.Find("LoadingManager").GetComponent<LoadingScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            loading.LoadLevel(sceneName);
        }
    }
}
