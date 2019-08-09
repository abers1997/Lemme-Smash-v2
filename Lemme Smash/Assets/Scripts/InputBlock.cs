using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBlock : MonoBehaviour
{
    [SerializeField]
    private KeyCode keyCode;

    private bool canHit;
    private bool hit;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    public Animator animator;

    public Action ValidHitCallback
    {
        get; set;
    }

    public Action MissCallback
    {
        get; set;
    }

    // Start is called before the first frame update
    void Awake()
    {
        hit = canHit = false;

        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("isAHit", 0);
        animator.SetInteger("isInputting", 0);

        if (Input.GetKeyDown(keyCode))
        {
            animator.SetInteger("isInputting", 1);

            if (canHit)
            {
                //Debug.Log("HIT!");
                ValidHitCallback();

                animator.SetInteger("isAHit", 1); //Tells the animator is a hit and not a miss for the animation

                // Replace this with better animation
                spriteRenderer.color = new Color(255f, 255f, 0f, 255f);

                canHit = false;
                hit = true;
            }
            else
            {
                MissCallback();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        canHit = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canHit = false;
        spriteRenderer.color = originalColor;

        if (!hit)
        {
            MissCallback();
        }
        else
        {
            hit = false;
        }
    }
}
