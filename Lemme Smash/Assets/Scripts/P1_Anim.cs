using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1_Anim : MonoBehaviour
{
    [SerializeField]
    private KeyCode leftKeyCode;

    [SerializeField]
    private KeyCode downKeyCode;

    [SerializeField]
    private KeyCode upKeyCode;

    [SerializeField]
    private KeyCode rightKeyCode;

    public Animator animator;

    void Update()
    {
        if (Input.GetKey(leftKeyCode))
        {
            animator.SetInteger("Left", 1);
        }

        else
        {
            animator.SetInteger("Left", 0);
        }

        if (Input.GetKey(downKeyCode))
        {
            animator.SetInteger("Down", 1);
        }

        else
        {
            animator.SetInteger("Down", 0);
        }

        if (Input.GetKey (upKeyCode))
        {
            animator.SetInteger("Up", 1);
        }

        else
        {
            animator.SetInteger("Up", 0);
        }

        

        if (Input.GetKey(rightKeyCode))
        {
            animator.SetInteger("Right", 1);
        }

        else
        {
            animator.SetInteger("Right", 0);
        }

        
    }
}
