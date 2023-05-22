using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloorTransition : MonoBehaviour
{
    [SerializeField] float fadeInTime;
    [SerializeField] float midTime;
    [SerializeField] float fadeOutTime;
    [SerializeField] float elapsedTime;
    [SerializeField] CanvasGroup cg;
    [SerializeField] bool active = false;

    void StartTransition()
    {
        active = true;
        elapsedTime = 0;
    }

    


    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onStairsReached += StartTransition;
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            elapsedTime += Time.deltaTime;

            if(elapsedTime > fadeInTime + midTime + fadeOutTime)
            {
                // Done
            }
            else if (elapsedTime > fadeInTime + midTime)
            {
                // Fading out
            }
            else if (elapsedTime > fadeInTime)
            {
                // Mid phase
            }
            else
            {
                // Fading in
            }
        }
    }
}
