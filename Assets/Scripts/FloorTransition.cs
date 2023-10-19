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
    [SerializeField] Phase phase;
    [SerializeField] bool active = false;

    void StartTransition()
    {
        active = true;
        elapsedTime = 0;
    }


    enum Phase
    {
        Inactive,
        FadeIn,
        Mid,
        FadeOut
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onStairsReached += StartTransition;
    }

    // Start is called before the first frame update
    void OnDestroy()
    {
        EventManager.instance.onStairsReached -= StartTransition;
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
                cg.alpha = 0;
                active = false;
                phase = Phase.Inactive;
                elapsedTime = 0;
                EventManager.instance.TransitionComplete();
            }
            else if (elapsedTime > fadeInTime + midTime)
            {
                // Fading out
                cg.alpha = 1 - (elapsedTime - (fadeInTime + midTime));
                phase = Phase.FadeOut;
            }
            else if (elapsedTime > fadeInTime)
            {
                if (phase == Phase.FadeIn)
                {
                    phase = Phase.Mid;
                    EventManager.instance.TransitionInProgress();
                }
                // Mid phase
                cg.alpha = 1;
            }
            else
            {
                // Fading in
                if(phase == Phase.Inactive)
                {
                    phase = Phase.FadeIn;
                }
                cg.alpha = elapsedTime / fadeInTime;
            }
        }
    }
}
