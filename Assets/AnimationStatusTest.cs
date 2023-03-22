using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStatusTest : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    public void ActivePhase()
    {
        Debug.Log("Active Phase Start");
    }
    public void EndPhase()
    {
        Debug.Log("End Phase Start");
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void SetAnimations(AnimatorOverrideController overrideController)
    {
        animator.runtimeAnimatorController = overrideController;
        return;
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
