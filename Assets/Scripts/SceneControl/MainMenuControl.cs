using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuControl : MonoBehaviour
{
    public enum MainMenuState
    {
        Title, Selection
    }

    public MainMenuState mainMenuState;

    public bool canTransition;

    public void enableTransitions ()
    {
        canTransition = true;
    }

    public void disableTransitions ()
    {
        canTransition = false;
    }

    private Animator animator;

    public void backToTitle ()
    {
        if (mainMenuState == MainMenuState.Selection && canTransition)
        {
            mainMenuState = MainMenuState.Title;
            animator.SetTrigger("Switch");
        }
    }

    private void Awake()
    {
        mainMenuState = MainMenuState.Title;

        canTransition = false;

        animator = /*transform.parent.*/GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))) {
            if (mainMenuState == MainMenuState.Title && canTransition)
            {
                mainMenuState = MainMenuState.Selection;
                animator.SetTrigger("Switch");
            } else if (mainMenuState == MainMenuState.Title && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f && !canTransition)
            {
                //animator.SetFloat("TimeSkip", 1f);
                animator.Play("Intro", 0, 1f);
                canTransition = true;
            }
        }
    }
}
