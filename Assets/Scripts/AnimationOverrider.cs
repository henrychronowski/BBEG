using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOverrider : MonoBehaviour
{
    Animator animator;
    
    // Start is called before the first frame update

    // Unsure if I will use these Phase functions or not but will leave them be for now
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

    public void SetAnimations(AnimatorOverrideController overrideController)
    {
        if (overrideController == null)
            return;

        animator.runtimeAnimatorController = overrideController;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
